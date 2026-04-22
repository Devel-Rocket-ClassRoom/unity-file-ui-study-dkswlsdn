using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSheetManager : MonoBehaviour
{
    public CharacterSheet sheet;
    public Transform contentTransform;
    public CharacterSummary summary;

    private List<CharacterSheet> sheetList = new List<CharacterSheet>();
    private List<SaveCharacterData> saveCharacterDataList = new List<SaveCharacterData>();



    private void OnEnable()
    {
        SetCharacterDataList(SaveLoadManager.Data.CharacterDataList);
    }

    private void OnDisable()
    {
        saveCharacterDataList = null;
    }

    void Load()
    {
        saveCharacterDataList =  SaveLoadManager.Data.CharacterDataList.ToList();
        var list = saveCharacterDataList.Where(filter[(int)filtering]).ToList();
        list.Sort(comparison[(int)sorting]);

        if (sheetList.Count < list.Count + 1)
        {
            for (int i = sheetList.Count; i < list.Count + 1; i++)
            {
                var newSheet = Instantiate(sheet, contentTransform);
                newSheet.sheetIndex = i;
                newSheet.SetEmpty();
                
                sheetList.Add(newSheet);
                sheetList[i].gameObject.SetActive(false);

                newSheet.deleteButton.onClick.AddListener(newSheet.RemoveData);
                newSheet.deleteButton.onClick.AddListener(newSheet.SetEmpty);
                newSheet.deleteButton.onClick.AddListener(Load);
                newSheet.unlockButton.onClick.AddListener(Load);
                newSheet.summary = summary;
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            if (i < list.Count)
            {
                sheetList[i].Load(list[i]);
                sheetList[i].gameObject.SetActive(true);
            }
        }

        for (int i = list.Count; i < sheetList.Count; i++)
        {
            sheetList[i].SetEmpty();
            sheetList[i].gameObject.SetActive(false);
        }

        sheetList[sheetList.Count - 1].gameObject.SetActive(true);
    }

    public void SetCharacterDataList(List<SaveCharacterData> list)
    {
        saveCharacterDataList = list.ToList();
        Load();
    }

    public void RefreshAll()
    {
        Load();
        summary.SetSummary();
    }

    public enum CharacterSortingOption
    {
        TimeAscending,
        TimeDescending,
        NameAscending,
        NameDescending,
        WeaponNameAscending,
        WeaponNameDescending,
    }

    public enum CharacterFilteringOption
    {
        NoneFiltering, Worrior, Tank, Archor, Wizard
    }

    public readonly System.Comparison<SaveCharacterData>[] comparison =
    {
        (lhs, rhs) => lhs.CreationTime.CompareTo(rhs.CreationTime),
        (lhs, rhs) => rhs.CreationTime.CompareTo(lhs.CreationTime),
        (lhs, rhs) => lhs.CharacterData.StringName.CompareTo(rhs.CharacterData.StringName),
        (lhs, rhs) => rhs.CharacterData.StringName.CompareTo(lhs.CharacterData.StringName),
        (lhs, rhs) =>
        {
            if (lhs.Weapon == rhs.Weapon) return 0;
            if (lhs.Weapon == null) return 1;
            if (rhs.Weapon == null) return -1;
            return lhs.Weapon.ItemData.Type.ToString().CompareTo(rhs.Weapon.ItemData.Type.ToString());
        },
        (lhs, rhs) =>
        {
            if (lhs.Weapon == rhs.Weapon) return 0;
            if (lhs.Weapon == null) return -1;
            if (rhs.Weapon == null) return 1;
            return rhs.Weapon.ItemData.Type.ToString().CompareTo(lhs.Weapon.ItemData.Type.ToString());
        }
    };

    public readonly System.Func<SaveCharacterData, bool>[] filter =
    {
        (x) => true,
        (x) => x.CharacterData.StringName == DataTableManager.CharacterTable.Get("Character1").StringName,
        (x) => x.CharacterData.StringName == DataTableManager.CharacterTable.Get("Character2").StringName,
        (x) => x.CharacterData.StringName == DataTableManager.CharacterTable.Get("Character3").StringName,
        (x) => x.CharacterData.StringName == DataTableManager.CharacterTable.Get("Character4").StringName,
    };

    protected CharacterSortingOption sorting = CharacterSortingOption.TimeAscending;
    protected CharacterFilteringOption filtering = CharacterFilteringOption.NoneFiltering;


    public CharacterSortingOption Sorting
    {
        get => sorting;
        set
        {
            if (sorting == value) return;
            sorting = value; Load();
        }
    }

    public CharacterFilteringOption Filtering
    {
        get => filtering;
        set
        {
            if (filtering == value) return;
            filtering = value; Load();
        }
    }
}
