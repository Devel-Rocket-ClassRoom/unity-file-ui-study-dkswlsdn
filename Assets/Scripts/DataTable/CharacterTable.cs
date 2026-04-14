using System.Collections.Generic;
using UnityEngine;

public class CharacterTable : DataTable
{
    private Dictionary<string, CharacterData> table = new Dictionary<string, CharacterData>();

    public override void Load(string fileName)
    {
        table.Clear();

        string path = string.Format(FormatPath, fileName);
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        List<CharacterData> list = LoadCSV<CharacterData>(textAsset.text);

        foreach (CharacterData item in list)
        {
            if (!table.ContainsKey(item.Id))
            {
                table.Add(item.Id, item);
            }
            else
            {
                Debug.LogError("캐릭터 아이디 중복");
            }
        }
    }

    public CharacterData Get(string id)
    {
        if (!table.ContainsKey(id))
        {
            return null;
        }

        return table[id];
    }
}

public class CharacterData
{
    public string Id {  get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public string Attack { get; set; }
    public string Defense { get; set; }
    public string Icon { get; set; }

    public string StringName => DataTableManager.StringTable.Get(Name);
    public string StringDesc => DataTableManager.StringTable.Get(Desc);
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");

}