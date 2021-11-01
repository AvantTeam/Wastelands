using System;
using System.IO;
using Newtonsoft.Json;

namespace wastelands.src
{
    public class SaveManager
    {
        private string savePath = Vars.path + "/save.json";
        public void Load()
        {
            if (!File.Exists(savePath)) return;

            using (StreamReader r = new StreamReader(savePath + "/save.json"))
            {
                string json = r.ReadToEnd();
                JSONData items = JsonConvert.DeserializeObject<JSONData>(json);
                Vars.settings.langCode = items.settings.langCode;
            }
        }

        public void Save()
        {
            if (!File.Exists(savePath)) File.Create(savePath);

            JsonSerializer serializer = JsonSerializer.Create();

            using (StreamWriter sw = new StreamWriter(savePath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, new JSONData());
            }
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
