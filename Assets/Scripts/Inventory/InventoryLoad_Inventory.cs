using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class InventoryLoad_Inventory : InventoryLoad
{
    public ItemDescription ItemDescription;

    private void Start()
    {
        onSelectButton.AddListener(SetDescription);
    }


    private void SetDescription(SaveItemData item)
    {
        if (ItemDescription == null || item == null) return;

        ItemDescription.SetItemData(item);
    }
}
