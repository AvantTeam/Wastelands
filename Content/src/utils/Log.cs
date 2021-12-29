using System;
using System.IO;

namespace wastelands.src.utils
{
    public static class Log
    {
        private static string logPath = Path.Combine(Vars.path, "log.txt");

        public static void Clear()
        {
            File.WriteAllText(logPath, string.Empty);
        }

        public static void Write(string data)
        {
            if (!File.Exists(logPath))
            {
                if (!Directory.Exists(Vars.aPath)) Directory.CreateDirectory(Vars.aPath);
                if (!Directory.Exists(Vars.path)) Directory.CreateDirectory(Vars.path);

                File.Create(logPath);
            }

            StreamWriter sr = File.AppendText(logPath);

            sr.WriteLine("[" + DateTime.Now.ToString("T") + "] " + data);

            sr.Close();
        }
    }
}
