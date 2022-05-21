using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EdgePolicyDownloader
{
    internal class EdgeFileDownloader
    {
        public FileInfo DownloadFile(Uri uri)
        {
            var tempFile = Path.GetTempFileName();
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(uri, tempFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            return new FileInfo(tempFile);
        }
    }
}
