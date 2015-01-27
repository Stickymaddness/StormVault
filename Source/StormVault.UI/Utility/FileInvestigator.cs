using System;
using System.IO;
using System.Threading;

namespace StormVault.UI.Utility
{
    public class FileInvestigator
    {
        public static bool IsFileFree(string path, int attempts)
        {
            try
            {
                int accessAttempts = 0;

                while (accessAttempts <= attempts)
                {
                    attempts++;
                    if (FileAvailable(path))
                        return true;

                    Thread.Sleep(500);
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool FileAvailable(string path)
        {
            FileStream stream = null;

            try
            {
                FileInfo file = new FileInfo(path);
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return false;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return true;
        }
    }
}
