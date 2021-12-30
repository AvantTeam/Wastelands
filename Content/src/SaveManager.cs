using Newtonsoft.Json;
using System;
using System.IO;
using wastelands.src.utils;

namespace wastelands.src
{
    public class SaveManager
    {
        public void Load()
        {
            try
            {
                if (!File.Exists(Vars.savePath)) return;

                using (StreamReader r = new StreamReader(Vars.savePath))
                {
                    string json = r.ReadToEnd();
                    JSONData items = JsonConvert.DeserializeObject<JSONData>(json);
                    Vars.settings.langCode = items.settings.langCode;
                }
            }
            catch (Exception) { }
        }

        public void Save()
        {
            try
            {
                if (!File.Exists(Vars.savePath))
                {
                    if (!Directory.Exists(Vars.avantPath)) Directory.CreateDirectory(Vars.avantPath);
                    if (!Directory.Exists(Vars.gamePath)) Directory.CreateDirectory(Vars.gamePath);

                    File.Create(Vars.savePath);
                }

                JsonSerializer serializer = JsonSerializer.Create();

                using (StreamWriter sw = new StreamWriter(Vars.savePath))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, new JSONData());
                }

                Log.Write("Data Saved.");
            }
            catch (Exception) { }
        }

        public class JSONData
        {
            public Settings settings;

            public JSONData()
            {
                settings = Vars.settings;
            }
        }
    }
}
