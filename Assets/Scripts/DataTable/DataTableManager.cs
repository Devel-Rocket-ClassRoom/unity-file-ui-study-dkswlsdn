using System.Collections.Generic;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

    public static StringTable StringTable => Get<StringTable>(DatableIds.String);
    public static ItemTable ItemTable => Get<ItemTable>(DatableIds.Item);
    public static CharacterTable CharacterTable => Get<CharacterTable>(DatableIds.Character);

    static DataTableManager()
    {
        Init();
    }

    public static void Init()
    {
#if !UNITY_EDITOR
        var stringTable = new StringTable();
        stringTable.Load(DataTableIds.String);
        tables.Add(DataTableIds.String, stringTable);
#else
        foreach (var id in DatableIds.StringTableIds)
        {
            var stringTable = new StringTable();
            stringTable.Load(id);
            tables.Add(id, stringTable);
        }
#endif

        var itemTable = new ItemTable();
        itemTable.Load(DatableIds.Item);
        tables.Add(DatableIds.Item, itemTable);

        var characterTable = new CharacterTable();
        characterTable.Load(DatableIds.Character);
        tables.Add(DatableIds.Character, characterTable);
    }

    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.LogError("테이블 없음");
            return null;
        }

        return tables[id] as T;
    }

#if UNITY_EDITOR
    public static StringTable GetStringTable(Languages lang)
    {
        return Get<StringTable>(DatableIds.StringTableIds[(int)lang]);
    }
#endif
}
