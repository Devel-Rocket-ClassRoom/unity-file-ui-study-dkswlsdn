using UnityEngine;
using SaveDataVC = SaveDataV3;
using Newtonsoft.Json;
using System.IO;
using System;

public static class SaveLoadManager
{
    public enum SaveMode
    {
        Text, Encrypted
    }

    public static SaveMode Mode { get; set; } = SaveMode.Encrypted;

    private static readonly string SaveDirectory = Path.Combine(Application.persistentDataPath, "Save");
    private static readonly string[] SaveFileNames =
    {
        "SateAuto",
        "Save1",
        "Save2",
        "Save3",
    };
    public static int SaveDataVersion { get; } = 3;
    public static SaveDataVC Data { get; set; } = new SaveDataVC();

    private static JsonSerializerSettings settings = new JsonSerializerSettings()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,
    };

    public static bool Save(int slot, SaveMode mode)
    {
        if (Data == null || slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }

        try
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            var json = JsonConvert.SerializeObject(Data, settings);
            string path = GetPath(slot, mode);

            switch (mode)
            {
                case SaveMode.Text:
                    File.WriteAllText(path, json);
                    break;
                case SaveMode.Encrypted:
                    File.WriteAllBytes(path, CryptoUtil.Encrypt(json));
                    break;
            }
            
            return true;
        }
        catch
        {
            Debug.LogError("Save 예외");
            return false;
        }
    }
    public static bool Save(int slot = 0) { return Save(slot, Mode); }

    public static bool Load(int slot, SaveMode mode)
    {
        if (slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }

        string path = GetPath(slot, mode);

        if (!File.Exists(path))
        {
            return false;
        }

        try
        {
            string json = string.Empty;

            switch (mode)
            {
                case SaveMode.Text:
                    json = File.ReadAllText(path);
                    break;
                case SaveMode.Encrypted:
                    json = CryptoUtil.Decrypt(File.ReadAllBytes(path));
                    break;
            }

            var data = JsonConvert.DeserializeObject<SaveData>(json, settings);
            while (data.Version < SaveDataVersion)
            {
                data = data.VersionUp();
            }
            Data = data as SaveDataVC;
            return true;
        }
        catch
        {
            Debug.LogError("Load 예외");
            return false;
        }
    }
    public static bool Load(int slot = 0) { return Load(slot, Mode); }

    private static string GetPath(int slot, SaveMode mode)
    {
        string ext = mode == SaveMode.Text ? ".json" : ".dat";
        
        return Path.Combine(SaveDirectory, $"{SaveFileNames[slot]}{ext}");
    }
}
