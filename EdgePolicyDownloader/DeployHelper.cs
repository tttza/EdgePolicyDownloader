using Microsoft.Deployment.Compression.Cab;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace EdgePolicyDownloader
{
    internal class DeployHelper
    {
        public static FileInfo ExtractCab(FileInfo file, string targetDirPath) {

            var cab = new CabInfo(file.FullName);
            cab.Unpack(targetDirPath);
            file.Delete();

            return new FileInfo(targetDirPath + @"\MicrosoftEdgePolicyTemplates.zip");
        }
        public static DirectoryInfo ExtractZip(FileInfo file, string targetDirPath)
        {

            var targetDir = new DirectoryInfo(targetDirPath);
            DeleteDirectory(targetDir);
            ZipFile.ExtractToDirectory(file.FullName.ToString(), targetDirPath);
            file.Delete();

            return new DirectoryInfo(targetDirPath);
        }

        public static bool DeleteDirectory(DirectoryInfo targetDir)
        {
            if (targetDir.Exists)
            {
                foreach (FileInfo f in targetDir.GetFiles())
                {
                    f.Delete();
                }
                foreach (DirectoryInfo dir in targetDir.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            return true;
        }

        // Use process to run as administrator.
        public static void CopyDirectoryXcopy(string sourceDir, string destinationDir)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Verb = "runas";
            startInfo.FileName = "xcopy";
            startInfo.Arguments = $"/E/H/Y {sourceDir} {destinationDir}";
            using (Process exeProcess = Process.Start(startInfo))
            {
                exeProcess.WaitForExit();
            }
        }

        public static bool MoveToPolicyDefinitions(DirectoryInfo directoryInfo)
        {
            var targetPath = @"C:\Windows\PolicyDefinitions";
            var sourcepath = Path.Combine(directoryInfo.FullName, @"windows\admx");
            try
            {
                CopyDirectoryXcopy(sourcepath, targetPath);
            } catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.InnerException);
                return false;
            }
            finally
            {
                DeleteDirectory(directoryInfo);
            }
            return true;
        }
    }
}
