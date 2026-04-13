using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Config : MonoBehaviour
{
    string path;
    Dictionary<string, string> config;

    void Start()
    {
        path = Path.Combine(Application.persistentDataPath, "settings.cfg");

        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        File.WriteAllText(path, @"master_volume=80
bgm_volume=70
sfx_volume=90
language=kr
show_damage=true");
    }

    // Update is called once per frame
    void Update()
    {
        LoadConfig();
        WriteConfig();
    }

    void LoadConfig()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            using (StreamReader sr = File.OpenText(path))
            {
                while (true)
                {
                    string line = sr.ReadLine();
                    if (line == null) break;

                    string[] cfg = line.Split('=');

                    if (cfg.Length != 2) { Debug.Log("파일 오류"); return; }

                    config.Add(cfg[0], cfg[1]);
                }
            }
        }
    }

    void WriteConfig()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            using (StreamWriter sr = File.CreateText(path))
            {
                for (int i = 0; i < config.Count; i++)
                {
                    var keys = config.Keys.ToString();
                   // string line = ;

                   // sr.WriteLine(line);
                }
            }
        }
    }
}
