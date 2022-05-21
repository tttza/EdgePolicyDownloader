using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EdgePolicyDownloader;

namespace EdgePolicyDownloader
{
    public partial class PolicyVersionSelector : Form
    {

        EdgeInfoDownloader edgeInfoDownloader = new EdgeInfoDownloader();

        public PolicyVersionSelector()
        {
            InitializeComponent();
        }

        public class EdgeReleaseData : EdgeRelease
        {
            public int MajorVersion { get; set; }
            public string ReleaseType { get; set; }


            public EdgeReleaseData(EdgeRelease edgeRelease)
            {
                ProductVersion = edgeRelease.ProductVersion;
                PublishedTime = edgeRelease.PublishedTime;
                ReleaseId = edgeRelease.ReleaseId;
                MajorVersion = int.Parse(edgeRelease.ProductVersion.Split('.')[0]);
            }

            public void AddReleaseType(int latestVersion, DateTime latestStableVerReleasedDate)
            {
                var latestStableVer = latestVersion - 2;
                if (MajorVersion == latestStableVer + 2)
                {
                    ReleaseType = "Canary";
                }
                else if (MajorVersion == latestStableVer + 1)
                {
                    ReleaseType = "Beta";
                }
                else if (MajorVersion > latestStableVer - 3)
                {
                    ReleaseType = "Stable";
                }
                else if ((MajorVersion == latestStableVer - 3) & ((MajorVersion) % 2 == 0))
                {
                    if (PublishedTime >= latestStableVerReleasedDate)
                    {
                        ReleaseType = "Extended";
                    }
                    else
                    {
                        ReleaseType = "(Extended)";
                    }
                }
                else 
                { 
                    ReleaseType = "EOS";
                }
            }
        }

        DateTime GetLatestMajorStableVersionReleasedData(List<EdgeReleaseData> releases)
        {
            var latestVersion = releases.Max(release => release.MajorVersion);
            var latestStableVer = latestVersion - 2;
            var publishedDate = releases.FindAll(release => release.MajorVersion == latestStableVer).Min(release => release.PublishedTime);
            return publishedDate;
        }

        List<String> columnsToHide = new List<string>() { "Architecture", "Platform", "Artifacts", "ExpectedExpiryDate", "CVEs"};

        private async void PolicyVersionSelector_LoadAsync(object sender, EventArgs e)
        {
            var policyReleases = edgeInfoDownloader.GetEdgePolicyReleases();

            var policyReleaseDatas = policyReleases.Select(release => new EdgeReleaseData(release)).ToList();

            var latestStableVerReleasedDate = GetLatestMajorStableVersionReleasedData(policyReleaseDatas);
            var latestVersion = policyReleaseDatas.Max(release => release.MajorVersion);
            policyReleaseDatas.ForEach(release => release.AddReleaseType(latestVersion, latestStableVerReleasedDate));
            

            this.EdgeReleasesGrid.DataSource = policyReleaseDatas;
            columnsToHide.ForEach(column => {
                if (EdgeReleasesGrid.Columns.Contains(column)) EdgeReleasesGrid.Columns[column].Visible = false;
             });

            EdgeReleasesGrid.Columns["MajorVersion"].DisplayIndex = 0;
            EdgeReleasesGrid.Columns["ReleaseType"].DisplayIndex = 1;

            EdgeReleasesGrid.Columns["ProductVersion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            EdgeReleasesGrid.Columns["ProductVersion"].DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);
            EdgeReleasesGrid.Columns["ProductVersion"].DisplayIndex = 2;

            EdgeReleasesGrid.Columns["ReleaseId"].DisplayIndex = 3;

            EdgeReleasesGrid.Columns["PublishedTime"].DefaultCellStyle.Padding = new Padding(5 ,0, 5, 0);
            EdgeReleasesGrid.Columns["PublishedTime"].DisplayIndex = 4;
        }
    }
}
