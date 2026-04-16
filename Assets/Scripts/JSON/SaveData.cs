using System.Collections.Generic;
using UnityEngine;

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
        throw new System.NotImplementedException();
    }
}