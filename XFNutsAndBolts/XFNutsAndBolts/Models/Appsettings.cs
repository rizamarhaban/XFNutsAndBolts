using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace XFNutsAndBolts.Models
{
    public class AppSettings
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName= "appversion", ItemConverterType = typeof(VersionConverter))]
        public Version AppVersion { get; set; }
    }
}
