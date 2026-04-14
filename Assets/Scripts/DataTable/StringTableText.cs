using UnityEngine;
using TMPro;

public class StringTableText : MonoBehaviour
{
    public string id;
    public TextMeshProUGUI text;

    private void Start()
    {
        OnChangedID();
    }

    private void OnChangedID()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }
}
