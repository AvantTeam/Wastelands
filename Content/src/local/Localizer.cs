using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;

namespace wastelands.src.local
{
    public static class Localizer
    {
        private static Dictionary<string, Dictionary<string, string>> values = new Dictionary<string, Dictionary<string, string>>();

        public static void LoadLocals(ContentManager manager)
        {
            string[] files = Directory.GetFiles(manager.RootDirectory + "/local");
            string[] content;

            foreach (string a in files)
            {
                using (Stream stream = TitleContainer.OpenStream(a))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        content = reader.ReadToEnd().Split("\n");

                        string p = "";
                        foreach (string b in content)
                        {
                            if (b.StartsWith("-"))
                            {
                                p = b.Substring(1).Replace("\n", "").Replace(" ", "").TrimEnd();
                                values.Add(p, new Dictionary<string, string>());
                            }
                            else
                            {
                                values[p].Add(b.Substring(1, 2), b.Substring(4));
                            }
                        }
                    }
                }
            }
        }

        public static string Get(string key)
        {
            try
            {
                return values[key][Vars.settings.langCode];
            }
            catch (Exception)
            {
                try
                {
                    return values[key]["EN"];
                }
                catch (Exception)
                {
                    return key;
                }
            }
        }
    }
}
