using TMPro;
using UnityEngine;

public class HeaderLocalization : Localization
{
    public TextMeshProUGUI header;
    public void ChangeText(string txt, string amount)
    {
        message = txt ?? "Unknown";
        StringTable table = DataTableManager.StringTable;
        text.text = table.Get(message ?? string.Empty);
        header.text = amount;
    }
}
