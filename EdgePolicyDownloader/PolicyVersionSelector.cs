using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        List<EdgeRelease> PolicyReleases = new List<EdgeRelease>();

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

        private async void PolicyVersionSelector_Shown(object sender, EventArgs e)
        {
            UpdateInfo("Loading info from server...");
            var policyReleaseDatas = new List<EdgeReleaseData>();
            var latestVersion = 0;
            await Task.Run(async () =>
            {
                PolicyReleases = await edgeInfoDownloader.GetEdgePolicyReleases();

                policyReleaseDatas = PolicyReleases.Select(release => new EdgeReleaseData(release)).ToList();
                
                latestVersion = policyReleaseDatas.Max(release => release.MajorVersion);
                var latestStableVerReleasedDate = GetLatestMajorStableVersionReleasedData(policyReleaseDatas);
                policyReleaseDatas.ForEach(release => release.AddReleaseType(latestVersion, latestStableVerReleasedDate));
            });
            this.EdgeReleasesGrid.DataSource = policyReleaseDatas;
            columnsToHide.ForEach(column =>
            {
                if (EdgeReleasesGrid.Columns.Contains(column)) EdgeReleasesGrid.Columns[column].Visible = false;
            });

            EdgeReleasesGrid.Columns["MajorVersion"].DisplayIndex = 0;
            EdgeReleasesGrid.Columns["ReleaseType"].DisplayIndex = 1;

            EdgeReleasesGrid.Columns["ProductVersion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            EdgeReleasesGrid.Columns["ProductVersion"].DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);
            EdgeReleasesGrid.Columns["ProductVersion"].DisplayIndex = 2;

            EdgeReleasesGrid.Columns["ReleaseId"].DisplayIndex = 3;

            EdgeReleasesGrid.Columns["PublishedTime"].DefaultCellStyle.Padding = new Padding(5, 0, 5, 0);
            EdgeReleasesGrid.Columns["PublishedTime"].DisplayIndex = 4;

            var selectIndex = policyReleaseDatas.FindIndex(r => r.MajorVersion == latestVersion - 2);
            EdgeReleasesGrid.Rows[selectIndex].Selected = true;
            UpdateInfo("");
        }

        private void EdgeReleasesGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cellIndex = EdgeReleasesGrid.Columns["ReleaseType"].Index;
            foreach (DataGridViewRow row in EdgeReleasesGrid.Rows)
            {   
                var cell = row.Cells[cellIndex];
                if ((string)cell.Value == "Stable") 
                {
                    cell.Style.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
                else if (((string)cell.Value).Contains("Extended"))
                {
                    cell.Style.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
                else if ((string)cell.Value == "Beta")
                {
                    cell.Style.BackColor = ColorTranslator.FromHtml("#8be2fd");
                }
                else if ((string)cell.Value == "Canary")
                {
                    cell.Style.BackColor = ColorTranslator.FromHtml("#f1c53e");
                }
                else if ((string)cell.Value == "EOS")
                {
                    cell.Style.BackColor = ColorTranslator.FromHtml("#dddddd");
                }
            }
        }

        private Uri GetUriFromId(int id)
        {
            var selectedRelease = PolicyReleases.Find(p => p.ReleaseId == id);
            var selectedUri = new Uri(selectedRelease.Artifacts.Find(a => a.ArtifactName == "cab").Location);
            return selectedUri;

        }
        
        private void UpdateInfo(string info)
        {
            InfoLabel.Text = info;
        }
        
        private void DeployButton_Click(object sender, EventArgs e)
        {
           
            var cellIndex = EdgeReleasesGrid.Columns["ReleaseId"].Index;
            var selectedId = (int)EdgeReleasesGrid.SelectedRows[0].Cells[cellIndex].Value;
            var selectedUri = GetUriFromId(selectedId);

            UpdateInfo("Downloading file...");
            var fileInfo = EdgeFileDownloader.DownloadFile(selectedUri);
            if (fileInfo == null)
            {
                UpdateInfo("Failed to downloading file.");
                return;
            }

            UpdateInfo("Extracting cab file...");
            var zipFileInfo = DeployHelper.ExtractCab(fileInfo, fileInfo.Directory.ToString());
            UpdateInfo("Extracting zip file...");
            var dirInfo = DeployHelper.ExtractZip(zipFileInfo, Path.Combine(zipFileInfo.Directory.ToString(), "ext"));
            UpdateInfo("Moving admx files to PolicyDefinitions directory. (Privilege Elevation is required. )");
            var isSuccess = DeployHelper.MoveToPolicyDefinitions(dirInfo);

            if (isSuccess)
            {
                UpdateInfo("Success.");
            }
            else
            {
                UpdateInfo("Failed to move admx files to PolicyDefinitions directory.");
            }
        }
    }
}
