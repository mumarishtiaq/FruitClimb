using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class JSONExtraction
{
    private static string settingsFileName = "GameSettings.json";
    public static void SaveSettingsIntoJSON(GameSettingsEntity currentSettings)
    {
        string json = JsonConvert.SerializeObject(currentSettings, Formatting.Indented);

        var path = GetCombinedPath();
        File.WriteAllText(path, json);
    }

    public static GameSettingsEntity FetchGameSettingsFromJSON()
    {
        var path = GetCombinedPath();
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<GameSettingsEntity>(json);
        }
        Debug.Log("File not exist");

        return new GameSettingsEntity();
    }
    private static string GetCombinedPath()
    {
        return Path.Combine(Application.persistentDataPath, settingsFileName);
    }

}
