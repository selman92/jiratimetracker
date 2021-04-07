using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using JiraTimeLogger.Jira;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TimeSpan = System.TimeSpan;

namespace JiraTimeLogger
{
    public partial class MainForm : Form
    {
        private readonly Timer _trackingTimer;
        private int _elapsedSeconds = 0;
        private bool _isTrackingStarted = false;
        private DateTime _startTime;
        private const string SettingsFileName = "settings.json";
        private const string DefaultElapsedTime = "00:00:00";
        private const string TempoApiUrl = "https://api.tempo.io/core/3/";
        private string _accountId;

        private readonly Regex _issueIdRegex =
            new Regex("[a-z0-9]*-\\d*", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public MainForm()
        {
            InitializeComponent();

            _trackingTimer = new Timer {Interval = 1000};
            _trackingTimer.Tick += TrackingTimerOnTick;

            LoadSettings();

            TxtApiToken.TextChanged += TxtApiTokenOnTextChanged;
            TxtApiToken.Validated += OnSettingsChange;

            PopulateWorkTypes();
            UpdateAccountId();
        }

        private async void UpdateAccountId()
        {
	        if (!string.IsNullOrEmpty(TxtApiToken.Text))
	        {
		        var client = GetJiraClient();

                _accountId = await Task.Run(() => client.GetAccountId());
            }
        }

        private void TxtApiTokenOnTextChanged(object sender, EventArgs e)
        {
	        PopulateWorkTypes();
	        UpdateAccountId();
        }

        private async void PopulateWorkTypes()
        {
	        if (!string.IsNullOrEmpty(TxtApiToken.Text))
	        {
		        var client = GetJiraClient();

		        var workTypes = await Task.Run(() =>  client.GetWorkTypes());

		        if (workTypes != null)
		        {
			        CmbWorktypes.DataSource = workTypes.ToList();
			        CmbWorktypes.DisplayMember = "Value";
			        CmbWorktypes.ValueMember = "Key";
                }
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
            var settingsObj = new {ApiToken = TxtApiToken.Text };

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
                _startTime = DateTime.Now;
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
            var values = new[] {TxtApiToken.Text, TxtIssueId.Text};

            return !values.Any(string.IsNullOrEmpty);
        }

        private void ResetAndStartTimer()
        {
            _elapsedSeconds = 0;
            _trackingTimer.Enabled = true;
            _trackingTimer.Start();
        }

        private async void BtnSubmit_Click(object sender, EventArgs e)
        {
            LblStatus.Text = "Saving the time log...";
            var jiraClient = GetJiraClient();

	        var issueId = GetIssueId(TxtIssueId.Text);

            var comment = TxtComment.Text;
            var workType = CmbWorktypes.SelectedValue.ToString();

            var success = await Task.Run(() => jiraClient.AddTimeLog(issueId, _accountId, _startTime, _elapsedSeconds,
                workType,
                comment));

	        if (success)
	        {
		        LblStatus.Text =
			        $"Time log successfully saved for the issue {GetIssueId(TxtIssueId.Text)}. Elapsed time: {Math.Max(_elapsedSeconds / 60, 1)} minutes.";
		        LblElapsedTime.Text = DefaultElapsedTime;
		        TxtComment.Text = string.Empty;
	        }
	        else
	        {
		        LblStatus.Text = "An error occurred while submitting the time log. Please check logs for details.";
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
            var jiraClient = new JiraClient(new Uri(TempoApiUrl), TxtApiToken.Text);
            return jiraClient;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            _elapsedSeconds = 0;
            LblElapsedTime.Text = DefaultElapsedTime;
            _startTime = DateTime.UtcNow;
        }
    }
}
