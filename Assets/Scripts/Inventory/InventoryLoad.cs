using NUnit.Framework;
using UnityEngine;

public class InventoryLoad : MonoBehaviour
{
    public InventoryButton button;
    public Transform inventory;
    public ItemDescription ItemDescription;

    private void OnEnable()
    {
        Load();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            var newItem = new SaveItemData();
            newItem.ItemData = DataTableManager.ItemTable.GetRandom();
            SaveLoadManager.Data.ItemId.Add(newItem);

            SaveLoadManager.Save();
        }
    }

    void Load()
    {
        if (SaveLoadManager.Load())
        {
            ResetButton();

            var list = SaveLoadManager.Data.ItemId;

            for (int i = 0; i < list.Count; i++)
            {
                var newButton = Instantiate(button, inventory);
                var desc = newButton.GetComponent<InventoryButton>();
                desc.SetItem(list[i]);
                desc.ItemDescription = ItemDescription;
            }
        }
    }

    void ResetButton()
    {
        for (int i = inventory.childCount - 1; i >= 0; i--)
        {
            Destroy(inventory.GetChild(i).gameObject);
        }
    }
}
