using System;
using System.IO;

namespace wastelands.src.utils
{
    public static class Log
    {
        public static void Clear()
        {
            if (File.Exists(Vars.logPath))
            {
                StreamWriter sw = new StreamWriter(Vars.logPath);
                sw.Close();
                sw.Dispose();
            }
        }

        public static void Write(params object[] data)
        {
            if (!File.Exists(Vars.logPath))
            {
                if (!Directory.Exists(Vars.avantPath)) Directory.CreateDirectory(Vars.avantPath);
                if (!Directory.Exists(Vars.gamePath)) Directory.CreateDirectory(Vars.gamePath);

                FileStream fs = File.Create(Vars.logPath);
                fs.Close();
                fs.Dispose();
            }

            StreamWriter sw = File.AppendText(Vars.logPath);

            string dataCompleted = "";
            foreach(object obj in data)
            {
                dataCompleted += obj.ToString() + " ";
            }

            dataCompleted = dataCompleted.Substring(0, dataCompleted.Length - 1);

            sw.WriteLine("[" + DateTime.Now.ToString("T") + "] " + dataCompleted);

            sw.Close();
            sw.Dispose();
        }
    }
}