using Newtonsoft.Json;

namespace Server.Utils
{
    public class FileIO
    {
        private const string JSON_FILE_DIR = "../../../Data/";

        public static void SaveJsonFile<T>(T obj, string fileName)
        {
            string fileFullPath = $"{JSON_FILE_DIR}{fileName}.json";

            DirectoryInfo directoryInfo = new DirectoryInfo(JSON_FILE_DIR);
            if (directoryInfo.Exists)
            {
                string outputJson = JsonConvert.SerializeObject(obj, Formatting.Indented);
                File.WriteAllText(fileFullPath, outputJson);
            }
        }

        public static T LoadJsonFile<T>(string fileName)
        {
            string fileFullPath = $"{JSON_FILE_DIR}{fileName}.json";

            if (!File.Exists(fileFullPath))
                return default(T);

            string json = File.ReadAllText(fileFullPath);
            T obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }
}
