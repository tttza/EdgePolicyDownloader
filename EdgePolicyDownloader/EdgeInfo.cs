using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;
using System.Text.RegularExpressions;

namespace EdgePolicyDownloader
{
    public partial class EdgeInfo
    {
        [JsonProperty("data")]
        public Datum[] Data { get; set; } = new Datum[0];

        [JsonIgnore]
        public string Layout { get; set; }

        [JsonIgnore]
        public string Fetch { get; set; }
        [JsonIgnore]
        public object Error { get; set; }

        [JsonIgnore]
        public State State { get; set; }

        [JsonIgnore]
        public bool? ServerRendered { get; set; }

        [JsonIgnore]
        public string RoutePath { get; set; }

        [JsonIgnore]
        public string Config { get; set; }

        [JsonIgnore]
        public string I18N { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("majorReleases")]
        public MajorRelease[] MajorReleases { get; set; }
    }

    public partial class MajorRelease
    {
        [JsonProperty("channelId")]
        public string ChannelId { get; set; }

        [JsonProperty("majorVersion")]
        public long MajorVersion { get; set; }

        [JsonProperty("releases")]
        public Release[] Releases { get; set; }
    }

    public partial class Release
    {
        [JsonProperty("fullVersion")]
        public string FullVersion { get; set; }

        [JsonProperty("policyUrl")]
        public Uri PolicyUrl { get; set; }

        [JsonProperty("platforms")]
        public Platform[] Platforms { get; set; }
    }

    public partial class Platform
    {
        [JsonProperty("platformId")]
        public string PlatformId { get; set; }

        [JsonProperty("downloadUrl")]
        public Uri DownloadUrl { get; set; }

        [JsonProperty("sizeInBytes")]
        public long SizeInBytes { get; set; }
    }

    

}
