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
        var list = saveCharacterDataList;
        Debug.Log(list.Count);

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
            Debug.Log(list[i].CharacterData.StringName);
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
}
