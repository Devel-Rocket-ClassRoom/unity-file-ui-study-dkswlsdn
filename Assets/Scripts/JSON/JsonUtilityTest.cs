using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonUtilityTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Playerinfo obj = new Playerinfo
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

            string path = Path.Combine(pathFolder, "player.json");

            string json = JsonUtility.ToJson(obj, prettyPrint: true);
            File.WriteAllText(path, json);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            string path = Path.Combine(Application.persistentDataPath, "JsonTest", "player.json");

            string json = File.ReadAllText(path);
            Playerinfo obj = new Playerinfo();
            JsonUtility.FromJsonOverwrite(json, obj);

            Debug.Log($"{obj.playerName}, {obj.lives}, {obj.health}, {obj.position}");
        }
    }
}

public class Playerinfo
{
    public string playerName;
    public int lives;
    public float health;
    public Vector3 position;
    public Dictionary<string, int> scores = new Dictionary<string, int>
    {
        {"stage1", 100 },
        {"stage2", 200 }
    };
}