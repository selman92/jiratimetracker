using System;
using Newtonsoft.Json;

namespace JiraTimeLogger.Jira
{
    internal class AddWorkLogModel
    {
        [JsonProperty("timeSpent")]
        public string ElapsedTime { get; set; }
        
        [JsonProperty("started")]
        public string StartedTime { get; set; }

        [JsonProperty("comment")]
        public object Comment { get; set; }
    }
}
