namespace Server.Data
{
    public interface ILoader<Key, Value>
    {
        Dictionary<Key, Value> MakeDict();
    }

    public class DataManager
    {
        public static Dictionary<int, WeaponItem> ItemDict { get; private set; } = new Dictionary<int, WeaponItem>();

        public static void LoadData()
        {
            ItemDict = LoadJson<Data.ItemData, int, WeaponItem>("ItemData").MakeDict();
        }
        
        static Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
        {
            string text = File.ReadAllText($"{ConfigManager.Config.dataPath}/{path}.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Loader>(text);
        }

    }
}
