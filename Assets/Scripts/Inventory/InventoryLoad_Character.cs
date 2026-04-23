using System;
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


    protected override void Load()
    {
        var list = saveItemDataList.Where(filter[(int)FilteringOption.NonConsumable]).ToList();
        list.Sort(comparison[(int)sorting]);

        if (buttonList.Count < list.Count + 1)
        {
            for (int i = buttonList.Count; i < list.Count + 1; i++)
            {
                var newButton = Instantiate(button, inventory);
                newButton.slotIndex = i;
                newButton.SetEmpty();
                newButton.gameObject.SetActive(false);

                newButton.button.onClick.AddListener(() =>
                {
                    selectedButtonIndex = newButton.slotIndex;
                    onSelectButton.Invoke(newButton.item);
                });

                buttonList.Add(newButton);
            }
        }

        for (int i = 1; i < buttonList.Count; i++)
        {
            if (i < list.Count)
            {
                buttonList[i].gameObject.SetActive(true);
                buttonList[i].SetItem(list[i]);
            }
            else
            {
                buttonList[i].gameObject.SetActive(false);
                buttonList[i].SetEmpty();
            }
        }

        selectedButtonIndex = -1;
        buttonList[0].gameObject.SetActive(true);
    }

    private void SetCharacterWeapon(SaveItemData item)
    {
        if (character == null)
            Debug.Log("scroll Null");
        if (character == null) return;

        character.Weapon = item;

        manager.RefreshAll();
        gameObject.SetActive(false);
        SaveLoadManager.Save();
    }
}
