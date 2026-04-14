using UnityEngine;

public class HeaderLocalization : Localization
{
    private string amount;
    public override void ChangeText(string txt, string amount)
    {
        if (amount == null) amount = this.amount;
        message = txt ?? "Unknown";
        StringTable table = DataTableManager.StringTable;
        string value = table.Get(message ?? string.Empty);
        text.text = value + " : " + amount;
        this.amount = amount;
    }
}
