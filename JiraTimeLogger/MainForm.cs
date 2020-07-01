using System;
using System.IO;
using System.Linq;
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

        public MainForm()
        {
            InitializeComponent();

            _trackingTimer = new Timer {Interval = 1000};
            _trackingTimer.Tick += TrackingTimerOnTick;

            LoadSettings();

            TxtApiToken.Validated += OnSettingsChange;
            TxtBaseUrl.Validated += OnSettingsChange;
            TxtEmail.Validated += OnSettingsChange;
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
            var jiraClient = new JiraClient(new Uri(TxtBaseUrl.Text), TxtApiToken.Text, TxtEmail.Text);

            if (jiraClient.AddTimeLog(TxtIssueId.Text.ToUpper(), _startTime, _elapsedSeconds, TxtComment.Text))
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

        private void TxtIssueId_TextChanged(object sender, EventArgs e)
        {
            BtnSubmit.Enabled = !string.IsNullOrEmpty(TxtIssueId.Text);
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            _elapsedSeconds = 0;
            LblElapsedTime.Text = DefaultElapsedTime;
            _startTime = DateTime.UtcNow;
        }
    }
}
