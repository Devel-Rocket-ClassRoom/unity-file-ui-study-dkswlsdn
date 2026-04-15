using UnityEngine;
using TMPro;
using UnityEditor;

public class Localization : MonoBehaviour
{
    [ContextMenuItem("언어 일괄 변경", "ChangeAllLanguage")]
#if UNITY_EDITOR
    public Languages languages;
#endif
    public string message;
    public TextMeshProUGUI text;


    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged += OnChanged;
            OnChanged();
        }
#if UNITY_EDITOR
        else
        { 
            OnChangeLanguage(languages);
        }
#endif
    }

     
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            OnChangeLanguage(languages); 
        }
    }

    private void OnChanged()
    {
        StringTable table = DataTableManager.StringTable;
        string value = table.Get(message ?? string.Empty);
        text.text = value;
    }

    public virtual void ChangeText(string txt)
    {
        message = txt ?? "Unknown";
        StringTable table = DataTableManager.StringTable;
        string value = table.Get(message ?? string.Empty);
        text.text = value;
    }

#if UNITY_EDITOR
    private void OnChangeLanguage(Languages lang)
    {
        var stringTable = DataTableManager.GetStringTable(lang);
        text.text = stringTable.Get(message);
    }

    private void ChangeAllLanguage()
    {
        Localization[] objects = FindObjectsByType<Localization>(FindObjectsSortMode.None);
        Variables.Languages = languages;

        foreach (Localization l in objects)
        {
            l.languages = languages;
            l.OnChanged();
        }
    }
#endif
}
