using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using Jurassic.Library;
using Newtonsoft.Json;
using RestSharp;

namespace EdgePolicyDownloader
{
     class EdgeInfoDownloader
    {
        protected EdgeInfo EdgeInfo { get; private set; } = new EdgeInfo();
        private async Task<EdgeInfo> DownloadParseEdgeInfo()
        {
            HtmlWeb web = new HtmlWeb();

            string downloadUrl = Uri.EscapeUriString("https://www.microsoft.com/en-us/edge/business/download");

            var doc = web.Load(downloadUrl);
            var script = doc.DocumentNode.Descendants()
                             .Where(n => n.Name == "script" & n.InnerText.Contains("window.__NUXT__="))
                             .First().InnerText;
            script = HttpUtility.HtmlDecode(script);


            var engine = new Jurassic.ScriptEngine();
            var result = engine.Evaluate(script.Substring(16));
            var infoJson = JSONObject.Stringify(engine, result).ToString();


            //var infoJson = doc.GetElementbyId("commercial-json-data").GetAttributeValue("data-whole-json", "[]");
            EdgeInfo edgeInfo = JsonConvert.DeserializeObject<EdgeInfo>(HttpUtility.HtmlDecode(infoJson));

            return edgeInfo;
        }
        public async Task<List<MajorRelease>> GetEdgePolicyReleases()
        {
            if (EdgeInfo.Data?.Length == 0)
            {
                EdgeInfo = await DownloadParseEdgeInfo();
            }
            var edgeReleases = EdgeInfo.Data.First().MajorReleases.ToList();

            return edgeReleases;
        }

    }
}
