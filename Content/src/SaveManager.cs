using Newtonsoft.Json;
using System.IO;

namespace wastelands.src
{
    public class SaveManager
    {
        public void Load()
        {
            if (!File.Exists(Vars.savePath)) return;

            using (StreamReader r = new StreamReader(Vars.savePath))
            {
                string json = r.ReadToEnd();
                JSONData items = JsonConvert.DeserializeObject<JSONData>(json);
                Vars.settings.langCode = items.settings.langCode;
            }
        }

        public void Save()
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
