using TMPro;
using UnityEngine;

public class UIPanelInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;
    public InventoryLoad InventoryLoad;




    private void OnEnable()
    {
        InventoryLoad.SetItemDataList(SaveLoadManager.Data.ItemDataList);
        OnSortingValueChange(sorting.value);
        OnFilteringValueChange(filtering.value);
    }

    public void OnSortingValueChange(int i)
    {
        InventoryLoad.Sorting = (InventoryLoad.SortingOption)i;
    }

    public void OnFilteringValueChange(int i)
    {
        InventoryLoad.Filtering = (InventoryLoad.FilteringOption)i;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.ItemDataList = InventoryLoad.GetSaveItemDataList();
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        InventoryLoad.SetItemDataList(SaveLoadManager.Data.ItemDataList);
    }

    public void OnCreateItem()
    {
        InventoryLoad.AddRandomItem();
    }

    public void OnRemoveItem()
    {
        InventoryLoad.RemoveItem();
    }

    public void SaveSortingFilteringSetting()
    {
        PlayerPrefs.SetInt("InventorySort", sorting.value);
        PlayerPrefs.SetInt("InventoryFilter", filtering.value);
    }

    public void LoadSortingFilteringSetting()
    {
        OnSortingValueChange(PlayerPrefs.GetInt("InventorySort"));
        sorting.value = PlayerPrefs.GetInt("InventorySort");
        OnFilteringValueChange(PlayerPrefs.GetInt("InventoryFilter"));
        filtering.value = PlayerPrefs.GetInt("InventoryFilter");
    }
}
