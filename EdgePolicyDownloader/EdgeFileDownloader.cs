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
        public static string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), "EPD");
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        public static FileInfo DownloadFile(Uri uri)
        {
            var tempFile = Path.Combine(GetTemporaryDirectory(), uri.ToString().Split('/').Last());
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(uri, tempFile);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                    Console.Error.WriteLine(ex.InnerException);
                    return null;
                }
            }
            return new FileInfo(tempFile);
        }
    }
}
