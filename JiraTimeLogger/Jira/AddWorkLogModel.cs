using System;
using Newtonsoft.Json;

namespace JiraTimeLogger.Jira
{
    internal class AddWorkLogModel
    {
        [JsonProperty("timeSpentSeconds")]
        public int ElapsedTimeInSeconds { get; set; }
        
        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("description")]
        public string Comment { get; set; }

        [JsonProperty("authorAccountId")]
        public string AccountId { get; set; }

        [JsonProperty("attributes")]
        public WorkAttribute[] Attributes { get; set; }

        [JsonProperty("issueKey")]
        public string IssueId { get; set; }
    }

    internal class WorkAttribute
    {
	    [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value  { get; set; }
    }
}
