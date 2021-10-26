using System.IO;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace wastelands.src.local
{
    public class Localizer
    {
        public Dictionary<string, Dictionary<string, string>> LoadLocals(ContentManager manager)
        {
            string[] files = Directory.GetFiles(manager.RootDirectory + "/local");
            string[] content;
            Dictionary<string, Dictionary<string, string>> o = new Dictionary<string, Dictionary<string, string>>();

            foreach (string a in files)
            {
                using (Stream stream = TitleContainer.OpenStream(a))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        content = reader.ReadToEnd().Split("\n");
                        Dictionary<string, string> temp = new Dictionary<string, string>();

                        foreach(string b in content)
                        {
                            if (!b.StartsWith("-"))
                            {
                                Console.WriteLine(b.Substring(0, 4) + " " + b.Substring(4));
                                try { temp.Add(b.Substring(0, 4), b.Substring(4)); } catch(Exception) { }
                            } else
                            {
                                Console.WriteLine(b);
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
