using System.Diagnostics;
using UnityEngine;

public enum Languages
{
    Korean,
    English,
    Japanese,
}

public enum ItemTypes
{
    Weapon,
    Equip,
    Consumable,
}

public static class Variables
{
    public static event System.Action OnLanguageChanged;
    

    private static Languages _languages = Languages.Korean;
    public static Languages Languages
    {
        get { return _languages; }
        set
        {
            if (_languages == value) return;
            _languages = value;
            OnLanguageChanged?.Invoke();
        }
    }
}

public static class DatableIds
{
    public static readonly string[] StringTableIds =
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp"
    };

    public static string String => StringTableIds[(int)Variables.Languages];

    public static readonly string Item = "ItemTable";
    public static readonly string Character = "CharacterTable";
}