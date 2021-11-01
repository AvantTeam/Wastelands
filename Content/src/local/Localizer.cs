using System.IO;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace wastelands.src.local
{
    public class Localizer
    {
        private Dictionary<string, Dictionary<string, string>> values = new Dictionary<string, Dictionary<string, string>>();

        public void LoadLocals(ContentManager manager)
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
                                Console.WriteLine(p);
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

        public string Get(string key)
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
