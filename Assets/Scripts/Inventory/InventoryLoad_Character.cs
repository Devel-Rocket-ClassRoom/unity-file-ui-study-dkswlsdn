using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class InventoryLoad_Character : InventoryLoad
{
    public SaveCharacterData character;
    public CharacterSheetManager manager;
    

    private void Start()
    {
        onSelectButton.AddListener(SetCharacterWeapon);
    }


    private void SetCharacterWeapon(SaveItemData item)
    {
        if (character == null)
            Debug.Log("scroll Null");
        if (character == null) return;

        character.Weapon = item;

        manager.RefreshAll();
        gameObject.SetActive(false);
    }
}
