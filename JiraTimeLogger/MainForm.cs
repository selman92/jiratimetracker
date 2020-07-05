using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using JiraTimeLogger.Jira;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TimeSpan = System.TimeSpan;

namespace JiraTimeLogger
{
    public partial class MainForm : Form
    {

        private readonly Timer _trackingTimer;
        private readonly Timer _issueTextChangeTimer;
        private int _elapsedSeconds = 0;
        private bool _isTrackingStarted = false;
        private DateTime _startTime;
        private const string SettingsFileName = "settings.json";
        private const string DefaultElapsedTime = "00:00:00";

        private readonly Regex _issueIdRegex =
            new Regex("[a-z0-9]*-\\d*", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public MainForm()
        {
            InitializeComponent();

            _trackingTimer = new Timer {Interval = 1000};
            _trackingTimer.Tick += TrackingTimerOnTick;

            _issueTextChangeTimer = new Timer {Interval = 500};
            _issueTextChangeTimer.Tick += IssueTextChangeTimerOnTick;

            LoadSettings();

            TxtApiToken.Validated += OnSettingsChange;
            TxtBaseUrl.Validated += OnSettingsChange;
            TxtEmail.Validated += OnSettingsChange;
        }

        private async void IssueTextChangeTimerOnTick(object sender, EventArgs e)
        {
            var issue = TxtIssueId.Text;

            if (!string.IsNullOrEmpty(issue))
            {
                var jiraClient = GetJiraClient();

                var issues = await Task.Run(() => jiraClient.GetIssues(issue));

                if (issues.Length > 0)
                {
                    TxtIssueId.DataSource = new[] {issue}.Concat(issues).ToArray();

                    TxtIssueId.Refresh();
                    TxtIssueId.DroppedDown = true;
                    TxtIssueId.Text = issue;
                }
               
                _issueTextChangeTimer.Stop();
                _issueTextChangeTimer.Enabled = false;
            }
        }


        private void TrackingTimerOnTick(object sender, EventArgs e)
        {
            _elapsedSeconds++;
            var elapsedTime = TimeSpan.FromSeconds(_elapsedSeconds);

            LblElapsedTime.Text = elapsedTime.ToString("hh\\:mm\\:ss");
        }

        private void OnSettingsChange(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            var settingsObj = new {ApiToken = TxtApiToken.Text, BaseUrl = TxtBaseUrl.Text, Email = TxtEmail.Text};

            var serialized = JsonConvert.SerializeObject(settingsObj);

            File.WriteAllText(SettingsFileName, serialized);
        }

        private void LoadSettings()
        {
            if (!File.Exists(SettingsFileName))
            {
                File.Create(SettingsFileName).Dispose();
                return;
            }

            try
            {
                var settingsObj = JObject.Parse(File.ReadAllText(SettingsFileName));

                TxtApiToken.Text = settingsObj["ApiToken"].ToString();
                TxtBaseUrl.Text = settingsObj["BaseUrl"].ToString();
                TxtEmail.Text = settingsObj["Email"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void BtnStartTrackingClick(object sender, EventArgs e)
        {
            if (!_isTrackingStarted)
            {
                _isTrackingStarted = true;
                _startTime = DateTime.UtcNow;
                BtnStartTracking.Text = "Stop Tracking";
                ResetAndStartTimer();
                BtnSubmit.Enabled = false;
            }
            else
            {
                _isTrackingStarted = false;
                BtnStartTracking.Text = "Start Tracking";
                _trackingTimer.Stop();
                _trackingTimer.Enabled = false;
                BtnSubmit.Enabled = ValidateForm();
            }
        }

        private bool ValidateForm()
        {
            var values = new[] {TxtApiToken.Text, TxtBaseUrl.Text, TxtEmail.Text, TxtIssueId.Text};

            return !values.Any(string.IsNullOrEmpty);
        }

        private void ResetAndStartTimer()
        {
            _elapsedSeconds = 0;
            _trackingTimer.Enabled = true;
            _trackingTimer.Start();
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            var jiraClient = GetJiraClient();

            var issueId = GetIssueId(TxtIssueId.Text);
            if (jiraClient.AddTimeLog(issueId, _startTime, _elapsedSeconds, TxtComment.Text))
            {
                LblStatus.Text = $"Time log successfully saved for the issue {TxtIssueId.Text}. Elapsed time: {Math.Max(_elapsedSeconds / 60, 1)} minutes.";
                LblElapsedTime.Text = DefaultElapsedTime;
                TxtComment.Text = string.Empty;
            }
            else
            {
                LblStatus.Text = "An error occurred while submitting the time log.";
            }
        }

        private string GetIssueId(string issueText)
        {
            var match = _issueIdRegex.Match(issueText);
            if (match.Success)
            {
                return match.Value;
            }

            return issueText;
        }

        private JiraClient GetJiraClient()
        {
            var jiraClient = new JiraClient(new Uri(TxtBaseUrl.Text), TxtApiToken.Text, TxtEmail.Text);
            return jiraClient;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            _elapsedSeconds = 0;
            LblElapsedTime.Text = DefaultElapsedTime;
            _startTime = DateTime.UtcNow;
        }

        private void TxtIssueId_SelectedValueChanged(object sender, EventArgs e)
        {
            _issueTextChangeTimer.Stop();

            TxtIssueId.SelectionLength = 0;
        }

        private void TxtIssueIdTextChanged(object sender, EventArgs e)
        {
            BtnSubmit.Enabled = !string.IsNullOrEmpty(TxtIssueId.Text);

            if (_issueTextChangeTimer.Enabled)
            {
                _issueTextChangeTimer.Stop();

                _issueTextChangeTimer.Start();
            }
            else
            {
                _issueTextChangeTimer.Enabled = true;
                _issueTextChangeTimer.Start();
            }

            if (string.IsNullOrEmpty(TxtIssueId.Text))
            {
                TxtIssueId.DataSource = Array.Empty<string>();
                TxtIssueId.Refresh();
                TxtIssueId.DroppedDown = false;
            }
        }
    }
}
