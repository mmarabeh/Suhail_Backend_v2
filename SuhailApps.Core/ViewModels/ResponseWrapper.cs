using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SuhailApps.Core.ViewModels
{
    public class SuccessResponseWrapper
    {
        [JsonProperty("data")]
        public object Data { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }


        [JsonProperty("status")]
        public bool Status { get; set; }
    }

    public class FailureResponseWrapper
    {
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }


        [JsonProperty("status")]
        public bool Status { get; set; }
    }
}
