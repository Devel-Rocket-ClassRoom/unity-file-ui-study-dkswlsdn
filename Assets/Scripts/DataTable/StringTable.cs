using UnityEngine;
using System.Collections.Generic;

public class StringTable : DataTable
{
    public class Data
    {
        public string Id { get; set; }
        public string String { get; set; }
    }

    private readonly Dictionary<string, string> table = new Dictionary<string, string>();

    public override void Load(string fileName)
    {
        table.Clear();
        
        string path = string.Format(FormatPath, fileName);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<Data>(textAsset.text);

        foreach (Data data in list)
        {
            if (!table.ContainsKey(data.Id))
            {
                table.Add(data.Id, data.String);
            }
            else
            {
                Debug.LogError($"Å° Áßº¹ : {data.Id}");
            }
        }
    }

    public static readonly string Unknown = "Unknown";

    public string Get(string key)
    {
        if (!table.ContainsKey(key))
        {
            return Unknown;
        }

        return table[key];
    }
}
