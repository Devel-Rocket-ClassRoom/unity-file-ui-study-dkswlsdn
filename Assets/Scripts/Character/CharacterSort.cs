using TMPro;
using UnityEngine;

public class CharacterSort : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;
    public CharacterSheetManager manager;




    private void OnEnable()
    {
        manager.SetCharacterDataList(SaveLoadManager.Data.CharacterDataList);
        OnSortingValueChange(sorting.value);
        OnFilteringValueChange(filtering.value);
    }

    public void OnSortingValueChange(int i)
    {
        manager.Sorting = (CharacterSheetManager.CharacterSortingOption)i;
    }

    public void OnFilteringValueChange(int i)
    {
        manager.Filtering = (CharacterSheetManager.CharacterFilteringOption)i;
    }


    public void SaveSortingFilteringSetting()
    {
        PlayerPrefs.SetInt("CharacterSort", sorting.value);
        PlayerPrefs.SetInt("CharacterFilter", filtering.value);
    }

    public void LoadSortingFilteringSetting()
    {
        OnSortingValueChange(PlayerPrefs.GetInt("CharacterSort"));
        sorting.value = PlayerPrefs.GetInt("CharacterSort");
        OnFilteringValueChange(PlayerPrefs.GetInt("CharacterFilter"));
        filtering.value = PlayerPrefs.GetInt("CharacterFilter");
    }
}
