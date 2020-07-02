using System;
using System.Globalization;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace JiraTimeLogger.Jira
{
    public class JiraClient
    {
        private const string RestApiBaseUrl = "/rest/api/3/";

        private readonly string WorklogUrl = $"{RestApiBaseUrl}issue/{{0}}/worklog";
        private readonly  string IssuesUrl = $"{RestApiBaseUrl}issue/picker";
        private readonly string _apiToken;
        private readonly Uri _baseUrl;
        private readonly string _email;

        private const string DateFormat = "yyyy-MM-ddTHH:mm:ss\\.fff";

        private const string CommentJsonFormat = @", ""comment"": {
    ""type"": ""doc"",
    ""version"": 1,
    ""content"": [
      {
        ""type"": ""paragraph"",
        ""content"": [
          {
            ""text"": ""#comment"",
            ""type"": ""text""
          }
        ]
      }
    ]
  } ";

        public JiraClient(Uri baseUrl, string apiToken, string email)
        {
            _apiToken = apiToken;
            _baseUrl = baseUrl;
            _email = email;
        }

        public bool AddTimeLog(string issueId, DateTime startTime, int elapsedSeconds, string comment = null)
        {
            var restClient = GetRestClient();

            var request = new RestRequest(new Uri(_baseUrl, string.Format(WorklogUrl, issueId)), Method.POST);

            request.JsonSerializer = new JsonNetSerializer();

            var elapsedTime = ConvertTime(elapsedSeconds);
            var addLogModel = new AddWorkLogModel
            {
                ElapsedTime = elapsedTime,
                StartedTime = startTime.ToString(DateFormat, CultureInfo.InvariantCulture) + "+0000",
            };

            var serializedModel = JsonConvert.SerializeObject(addLogModel);

            if (!string.IsNullOrEmpty(comment))
            {
                // An ugly hack to add comment by modifying raw JSON
                // Because I'm too lazy to declare model class for 3 nested levels of json for one piece of text.
                serializedModel = AddComment(serializedModel, comment);
            }

            var deserializedObj = JsonConvert.DeserializeObject<AddWorkLogModel>(serializedModel);

            request.AddJsonBody(deserializedObj, "application/json");

            try
            {
                var response = restClient.Execute(request);

                return response.StatusCode == HttpStatusCode.Created;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }

        public string[] GetIssues(string searchKeyword)
        {
            var restClient = GetRestClient();

            var request = new RestRequest(new Uri(_baseUrl, IssuesUrl), Method.GET);

            request.AddParameter("query", searchKeyword, ParameterType.QueryString);

            var response = restClient.Execute(request);

            var rawIssues = JObject.Parse(response.Content);

            var issues = rawIssues["sections"][0]["issues"].ToArray().Select(issue =>
                issue["key"].Value<string>() + " - " + issue["summary"].Value<string>()).ToArray();

            return issues;
        }

        private IRestClient GetRestClient()
        {
            var restClient = new RestClient(_baseUrl);
            restClient.Authenticator = new HttpBasicAuthenticator(_email, _apiToken);

            return restClient;
        }

        private string AddComment(string serializedModel, string comment)
        {
            var lastBraceIndex = serializedModel.LastIndexOf('}');

            var commentJson = CommentJsonFormat.Replace("#comment", comment);

            serializedModel = serializedModel.Insert(lastBraceIndex, commentJson);

            return serializedModel;
        }

        private string ConvertTime(in int elapsedSeconds)
        {
            return Math.Max(elapsedSeconds / 60, 1) + "m";
        }
    }
}
