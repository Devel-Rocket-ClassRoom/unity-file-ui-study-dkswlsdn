using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonTest1 : MonoBehaviour
{
    private JsonSerializerSettings jsonSetting;

    private void Awake()
    {
        jsonSetting = new JsonSerializerSettings();
        jsonSetting.Formatting = Formatting.Indented;
        jsonSetting.Converters.Add(new Vector3Converter());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerState obj = new PlayerState
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f,
                position = new Vector3(1f, 2f, 3f)
            };

            string pathFolder = Path.Combine(Application.persistentDataPath, "JsonTest");
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }

            string path = Path.Combine(pathFolder, "player2.json");

            string json = JsonConvert.SerializeObject(obj, Formatting.Indented, jsonSetting);
            File.WriteAllText(path, json);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            string path = Path.Combine(Application.persistentDataPath, "JsonTest", "player2.json");

            
            string json = File.ReadAllText(path);
            PlayerState obj = JsonConvert.DeserializeObject<PlayerState>(json, jsonSetting);

            Debug.Log(obj.ToString());
        }
    }
}

[Serializable]
public class PlayerState
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 position;

    public override string ToString()
    {
        return $"{playerName} / {lives} / {health} / {position}";
    }
}