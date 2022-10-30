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
        List<EdgeReleaseData> _policyReleaseDatas;
        List<MajorRelease> MajorReleases = new List<MajorRelease>();

        public PolicyVersionSelector()
        {
            InitializeComponent();
        }

        public class EdgeReleaseData
        {
            public long MajorVersion { get; set; }
            //public string ReleaseType { get; set; }
            public Version ProductVersion { get; set; }
            public string Channel { get; set; }
            public Uri DownloadUri { get; set; }


            public EdgeReleaseData(long mv, string channel, Release edgeRelease)
            {
                ProductVersion = new Version(edgeRelease.FullVersion);
                MajorVersion = mv;
                Channel = channel;
                DownloadUri = edgeRelease.PolicyUrl;
            }
        }

        int GetLatestMajorStableVersion(List<EdgeReleaseData> releases)
        {
            int latestStableVer = (int)releases.Find(r => r.Channel == "stable").MajorVersion;   
            return latestStableVer;
        }

        List<String> columnsToHide = new List<string>() { "Architecture", "Platform", "Artifacts", "ExpectedExpiryDate", "CVEs"};

        private async void PolicyVersionSelector_Shown(object sender, EventArgs e)
        {
            UpdateInfo("Loading info from server...");
            var policyReleaseDatas = new List<EdgeReleaseData>();
            var latestMajorVersion = 0;
            await Task.Run(async () =>
            {
                MajorReleases = await edgeInfoDownloader.GetEdgePolicyReleases();

                policyReleaseDatas = MajorReleases.Select(release => {
                    var mv = release.MajorVersion;
                    var channel = release.ChannelId;
                    return release.Releases.Select(r => new EdgeReleaseData(mv, channel, r)).ToList();
                    }
                    ).SelectMany(i => i).ToList(); 
                
                
            });
            latestMajorVersion = GetLatestMajorStableVersion(policyReleaseDatas);


            policyReleaseDatas = policyReleaseDatas.OrderByDescending(r => r.ProductVersion).ToList();
            this.EdgeReleasesGrid.DataSource = policyReleaseDatas;
            columnsToHide.ForEach(column =>
            {
                if (EdgeReleasesGrid.Columns.Contains(column)) EdgeReleasesGrid.Columns[column].Visible = false;
            });

            EdgeReleasesGrid.Columns["MajorVersion"].DisplayIndex = 0;

            EdgeReleasesGrid.Columns["ProductVersion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            EdgeReleasesGrid.Columns["ProductVersion"].DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);
            EdgeReleasesGrid.Columns["ProductVersion"].DisplayIndex = 2;

            EdgeReleasesGrid.Columns["DownloadUri"].Visible = false;


            var selectIndex = policyReleaseDatas.FindIndex(r => (r.MajorVersion == latestMajorVersion) && (r.Channel == "stable"));
            EdgeReleasesGrid.Rows[selectIndex].Selected = true;
            UpdateInfo("");
            _policyReleaseDatas = policyReleaseDatas;
        }

        private void EdgeReleasesGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cellIndex = EdgeReleasesGrid.Columns["Channel"].Index;
            foreach (DataGridViewRow row in EdgeReleasesGrid.Rows)
            {   
                var cell = row.Cells[cellIndex];
                var cellValue = (string)cell.Value;
                if (cellValue == "stable") 
                {
                    cell.Style.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
                else if (!(cellValue is null)  && (cellValue.Contains("extended")))
                {
                    cell.Style.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
                else if (cellValue == "beta")
                {
                    cell.Style.BackColor = ColorTranslator.FromHtml("#8be2fd");
                }
                else if (cellValue == "dev")
                {
                    cell.Style.BackColor = ColorTranslator.FromHtml("#f1c53e");
                }
                else if (cellValue == "EOS")
                {
                    cell.Style.BackColor = ColorTranslator.FromHtml("#dddddd");
                }
            }
        }

        
        private void UpdateInfo(string info)
        {
            InfoLabel.Text = info;
        }

        private Uri GetUri(Version version)
        {
            var selectedRelease = _policyReleaseDatas.Find(p => p.ProductVersion == version);
            var selectedUri = selectedRelease.DownloadUri;
            return selectedUri;

        }

        private void DeployButton_Click(object sender, EventArgs e)
        {
           
            var cellIndex = EdgeReleasesGrid.Columns["ProductVersion"].Index;
            Version selectedVersion = (Version)EdgeReleasesGrid.SelectedRows[0].Cells[cellIndex].Value;
            var selectedUri = GetUri(selectedVersion);

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
    public class SortPara : IComparable<SortPara>
    {
        public List<int> numbers { get; set; }
        public string strNumbers { get; set; }
        public SortPara(string strNumbers)
        {
            this.strNumbers = strNumbers;
            numbers = strNumbers.Split(new char[] { '.' }).Select(x => int.Parse(x)).ToList();

        }
        public int CompareTo(SortPara other)
        {
            int shortest = this.numbers.Count < other.numbers.Count ? this.numbers.Count : other.numbers.Count;
            int results = 0;
            for (int i = 0; i < shortest; i++)
            {
                if (this.numbers[i] != other.numbers[i])
                {
                    results = this.numbers[i].CompareTo(other.numbers[i]);
                    break;
                }
            }
            return results;
        }
    }
}
