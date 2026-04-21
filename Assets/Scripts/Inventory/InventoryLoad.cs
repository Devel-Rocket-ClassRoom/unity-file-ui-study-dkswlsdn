using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class InventoryLoad : MonoBehaviour
{
    public InventoryButton button;
    public Transform inventory;

    protected List<InventoryButton> buttonList = new List<InventoryButton>();
    protected List<SaveItemData> saveItemDataList = new List<SaveItemData>();


    private void OnEnable()
    {
        SetItemDataList(SaveLoadManager.Data.ItemDataList);
    }

    private void OnDisable()
    {
        saveItemDataList = null;
    }




    void Load()
    {
        var list = saveItemDataList.Where(filter[(int)filtering]).ToList();
        list.Sort(comparison[(int)sorting]);

        if (buttonList.Count < list.Count)
        {
            for (int i = buttonList.Count; i < list.Count; i++)
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

        for (int i = 0; i < buttonList.Count; i++)
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
    }

    public void SetItemDataList(List<SaveItemData> list)
    {
        saveItemDataList = list.ToList();
        Load();
    }

    public enum SortingOption
    {
        TimeAscending,
        TimeDescending,
        NameAscending,
        NameDescending,
        TypeAscending,
        TypeDescending,
    }

    public enum FilteringOption
    {
        NoneFiltering, Weapon, Equip, Consumable, NonConsumable
    }

    public readonly System.Comparison<SaveItemData>[] comparison =
    {
        (lhs, rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime),
        (lhs, rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime),
        (lhs, rhs) => lhs.ItemData.StringName.CompareTo(rhs.ItemData.StringName),
        (lhs, rhs) => rhs.ItemData.StringName.CompareTo(lhs.ItemData.StringName),
        (lhs, rhs) => lhs.ItemData.Type.ToString().CompareTo(rhs.ItemData.Type.ToString()),
        (lhs, rhs) => rhs.ItemData.Type.ToString().CompareTo(lhs.ItemData.Type.ToString()),
    };

    public readonly System.Func<SaveItemData, bool>[] filter =
    {
        (x) => true,
        (x) => x.ItemData.Type == ItemTypes.Weapon,
        (x) => x.ItemData.Type == ItemTypes.Equip,
        (x) => x.ItemData.Type == ItemTypes.Consumable,
        (x) => x.ItemData.Type != ItemTypes.Consumable,
    };

    private SortingOption sorting = SortingOption.TimeAscending;
    private FilteringOption filtering = FilteringOption.NoneFiltering;
    

    public SortingOption Sorting
    {
        get => sorting;
        set
        {
            if (sorting == value) return;
            sorting = value; Load();
        }
    }

    public FilteringOption Filtering
    {
        get => filtering;
        set
        {
            if (filtering == value) return;
            filtering = value; Load();
        }
    }

    private int selectedButtonIndex = -1;
    public UnityEvent onUpdateButton;
    public UnityEvent<SaveItemData> onSelectButton;





    public void AddRandomItem()
    {
        saveItemDataList.Add(SaveItemData.GetRandomItem());
        Load();
    }
    public void RemoveItem()
    {
        if (selectedButtonIndex == -1) { return; }

        saveItemDataList.Remove(buttonList[selectedButtonIndex].item);
        Load();
    }

    public List<SaveItemData> GetSaveItemDataList()
    {
        return saveItemDataList;
    }
}
