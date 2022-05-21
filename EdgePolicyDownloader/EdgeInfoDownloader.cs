using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace EdgePolicyDownloader
{
     class EdgeInfoDownloader
    {
        protected List<EdgeInfo> EdgeInfos { get; private set; } = new List<EdgeInfo>();
        private async Task<List<EdgeInfo>> DownloadParseEdgeInfo()
        {
            HtmlWeb web = new HtmlWeb();

            string downloadUrl = Uri.EscapeUriString("https://www.microsoft.com/en-us/edge/business/download");

            var doc = web.Load(downloadUrl);
            var infoJson = doc.GetElementbyId("commercial-json-data").GetAttributeValue("data-whole-json", "[]");
            List<EdgeInfo> edgeInfos = JsonConvert.DeserializeObject<List<EdgeInfo>>(HttpUtility.HtmlDecode(infoJson));

            return edgeInfos;
        }
        public List<EdgeRelease> GetEdgePolicyReleases()
        {
            if (EdgeInfos.Count == 0)
            {
                EdgeInfos = DownloadParseEdgeInfo().Result;
            }
            var edgeReleases = EdgeInfos.Find(e => e.Product == "Policy").Releases;

            return edgeReleases;
        }

    }
}
