using System;
using System.IO;

namespace StormVault.Core
{
    public static class Logger
    {
        private const string OUTPUT = "error.log";
        private static object fileLock = new object();
        public static void Log(Exception e)
        {
            Log(e.ToString());
        }
        public static void Log(string message)
        {
            lock (fileLock)
                File.AppendAllText(OUTPUT, string.Format("{0}[{1}] {2}", Environment.NewLine, DateTime.Now.ToString("dd-MM-yyyy H:mm"), message));
        }
    }
}
