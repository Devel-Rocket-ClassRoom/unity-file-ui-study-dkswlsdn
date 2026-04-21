using System;
using Newtonsoft.Json;
using UnityEngine;

public class SaveCharacterData
{
    public Guid InstanceId { get; set; }
    public CharacterData CharacterData { get; set; }
    public SaveItemData Weapon { get; set; }
    public DateTime CreationTime { get; set; }

    public SaveCharacterData()
    {
        InstanceId = Guid.NewGuid();
        CreationTime = DateTime.Now;
    }

    public static SaveCharacterData GetRandomCharacter()
    {
        var newItem = new SaveCharacterData();
        var item = DataTableManager.CharacterTable.GetRandom();
        newItem.CharacterData = item;
        newItem.Weapon = null;
        return newItem;
    }
}
