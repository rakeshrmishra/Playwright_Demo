using Microsoft.Playwright;
using System;
using System.Collections.Generic;

using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

namespace Utils{
    public partial class DEMOConfig
    {
        // [JsonProperty("username")]
        // public string Username { get; set; }

        // [JsonProperty("password")]
        // public string Password { get; set; }

        [JsonProperty("browserOptions")]
        public BrowserTypeLaunchOptions BrowserOptions { get; set; }

        [JsonProperty("browserContextOptions")]
        public BrowserNewContextOptions BrowserContextOptions { get; set; }

        [JsonProperty("tracingStartOptions")]
        public TracingStartOptions TracingStartOptions { get; set; }

        [JsonProperty("tracingStopOptions")]
        public TracingStopOptions TracingStopOptions { get; set; }

    }

}