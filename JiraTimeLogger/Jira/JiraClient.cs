using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Newtonsoft.Json.Linq;
using NLog;
using NLog.Fluent;
using RestSharp;

namespace JiraTimeLogger.Jira
{
	public class JiraClient
	{
		private const string WorklogsEndpoint = "/worklogs";

		private const string WorkAttributeKey = "_Type_";
        private const string DateFormat = "yyyy-MM-dd";
        private const string TimeFormat = "HH:mm:ss";

        private readonly string _apiToken;

		private readonly Uri _baseUrl;
		private readonly string WorkAttributesEndpoint = "/work-attributes";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

		public JiraClient(Uri baseUrl, string apiToken)
		{
			_apiToken = apiToken;
			_baseUrl = baseUrl;
		}

		public bool AddTimeLog(string issueId, string accountId, DateTime startTime, int elapsedSeconds,
			string workType, string comment = null)
		{
			var restClient = GetRestClient();

			var request = new RestRequest(new Uri(string.Concat(_baseUrl.AbsoluteUri.Trim('/'), WorklogsEndpoint)),
				Method.POST);

			request.JsonSerializer = new JsonNetSerializer();

			var addLogModel = new AddWorkLogModel
			{
				IssueId = issueId,
				ElapsedTimeInSeconds = elapsedSeconds,
				StartDate = startTime.ToString(DateFormat, CultureInfo.InvariantCulture),
				StartTime = startTime.ToString(TimeFormat, CultureInfo.InvariantCulture),
				Comment = comment ?? string.Empty,
				AccountId = accountId,
				Attributes = new[]
				{
					new WorkAttribute
					{
						Key = WorkAttributeKey,
						Value = workType
					}
				}
			};

			AddAuthHeader(request);

			request.AddJsonBody(addLogModel, "application/json");

            request.Timeout = 120000;

			try
			{
				var response = restClient.Execute(request);

				if(response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }


                Logger.Log(LogLevel.Error, $"An error occurred while saving time log, details: {response.Content}");
            }
			catch (Exception ex)
			{
                Logger.Log(LogLevel.Error, ex);
			}

			return false;
		}

		private IRestClient GetRestClient()
		{
			var restClient = new RestClient(_baseUrl);

			return restClient;
		}

		public Dictionary<string, string> GetWorkTypes()
		{
			var restClient = GetRestClient();

			var request = new RestRequest(new Uri(_baseUrl.AbsoluteUri.TrimEnd('/') + WorkAttributesEndpoint),
				Method.GET);

			AddAuthHeader(request);

			try
			{
				var response = restClient.Execute(request);

				var rawResponse = JObject.Parse(response.Content);

				var workTypeNames = rawResponse["results"][0]["names"].ToObject<JObject>();

				var workTypes = new Dictionary<string, string>();
				foreach (var prop in workTypeNames.Properties()) workTypes.Add(prop.Name, prop.Value.Value<string>());

				return workTypes;
			}
			catch (Exception ex)
			{
                Logger.Log(LogLevel.Error, ex);
				return null;
			}
		}

		private void AddAuthHeader(RestRequest request)
		{
			request.AddHeader("Authorization", $"Bearer {_apiToken}");
		}

		public string GetAccountId()
		{
			var restClient = GetRestClient();

			var request = new RestRequest(new Uri(_baseUrl.AbsoluteUri.TrimEnd('/') + WorklogsEndpoint), Method.GET);

			request.Parameters.Add(new Parameter("limit", "1", ParameterType.QueryString));

			AddAuthHeader(request);

			try
			{
				var response = restClient.Execute(request);

				var rawResponse = JObject.Parse(response.Content);

				var accountId = rawResponse["results"][0]["author"]["accountId"];

				return accountId.Value<string>();
			}
			catch (Exception ex)
			{
				Logger.Log(LogLevel.Error, ex);
				return null;
			}
		}
	}
}