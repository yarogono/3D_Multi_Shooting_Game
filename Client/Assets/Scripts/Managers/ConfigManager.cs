using System;
using UnityEngine;

[Serializable]
public class ServerConfig
{
    public string dbConnectionString;
    public string gameServerIpAddr;
    public int gameServerPort;
    public string restApiUrl;
}

public class ConfigManager
{
    public static ServerConfig Config { get; private set; }

    public static void LoadConfig()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Config/config");
        Config = Newtonsoft.Json.JsonConvert.DeserializeObject<ServerConfig>(textAsset.text);
    }
}
