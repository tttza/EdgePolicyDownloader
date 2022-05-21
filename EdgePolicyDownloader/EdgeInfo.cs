using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgePolicyDownloader
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class EdgeArtifact
    {
        public string ArtifactName { get; set; }
        public string Hash { get; set; }
        public string HashAlgorithm { get; set; }
        public string Location { get; set; }
        public int SizeInBytes { get; set; }
    }

    public class EdgeRelease
    {
        public string Architecture { get; set; }
        public List<EdgeArtifact> Artifacts { get; set; }
        public List<string> CVEs { get; set; }
        public DateTime ExpectedExpiryDate { get; set; }
        public string Platform { get; set; }
        public string ProductVersion { get; set; }
        public DateTime PublishedTime { get; set; }
        public int ReleaseId { get; set; }
    }

    public class EdgeInfo
    {
        public string Product { get; set; }
        public List<EdgeRelease> Releases { get; set; }
    }

}
