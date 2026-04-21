using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public abstract class SaveData
{
    public int Version { get; protected set; }
    public abstract SaveData VersionUp();
}

[System.Serializable]
public class SaveDataV1 : SaveData
{
    public string PlayerName { get; set; } = string.Empty;
    public SaveDataV1()
    {
        Version = 1;
    }
    public override SaveData VersionUp()
    {
        var nextVersion = new SaveDataV2();
        nextVersion.Name = PlayerName;
        return nextVersion;
    }
}

[System.Serializable]
public class SaveDataV2 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;

    public SaveDataV2()
    {
        Version = 2;
    }
    public override SaveData VersionUp()
    {
        var nextVersion = new SaveDataV3();
        nextVersion.Name = Name;
        nextVersion.Gold = Gold;
        return nextVersion;
    }
}

[System.Serializable]
public class SaveDataV3 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;
    public List<string> ItemId = new List<string>();

    public SaveDataV3()
    {
        Version = 3;
    }
    public override SaveData VersionUp()
    {
        var nextVersion = new SaveDataV4();
        nextVersion.Name = Name;
        nextVersion.Gold = Gold;

        foreach (var item in ItemId)
        {
            SaveItemData itemData = new SaveItemData();
            itemData.ItemData = DataTableManager.ItemTable.Get(item);
            nextVersion.ItemDataList.Add(itemData);
        }

        return nextVersion;
    }
}

[System.Serializable]
public class SaveDataV4 : SaveDataV2
{
    public List<SaveItemData> ItemDataList = new List<SaveItemData>();

    public SaveDataV4()
    {
        Version = 4;
    }
    public override SaveData VersionUp()
    {
        var nextVersion = new SaveDataV5();
        nextVersion.Name = Name;
        nextVersion.Gold = Gold;

        foreach (var item in ItemDataList)
        {
            nextVersion.ItemDataList.Add(item);
        }

        return nextVersion;
    }
}

[System.Serializable]
public class SaveDataV5 : SaveDataV4
{
    public List<SaveCharacterData> CharacterDataList = new List<SaveCharacterData>();

    public SaveDataV5()
    {
        Version = 5;
    }
    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}