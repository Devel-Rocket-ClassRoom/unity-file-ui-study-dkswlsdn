using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : ShowItemDescription
{
    public static readonly string FormatCommon = "{0} : {1}";

    public TextMeshProUGUI textType;
    public TextMeshProUGUI instanceId;
    public TextMeshProUGUI textValue;
    public TextMeshProUGUI textCost;
    public TextMeshProUGUI creationTime;

    public void SetItemData(SaveItemData data)
    {
        if (data == null) return;

        image.sprite = data.ItemData.SpriteIcon;
        text.ChangeText(data.ItemData.Name);
        desc.ChangeText(data.ItemData.Desc);
        instanceId.text = $"ID : {data.InstanceId.ToString()}";
        creationTime.text = $"Creation at : {data.CreationTime.ToString()}";

        textType.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Type"), data.ItemData.Type);
        textValue.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Value"), data.ItemData.Value);
        textCost.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Cost"), data.ItemData.Cost);
    }
    public new void SetItemData(string id)
    {
        var newItem = new SaveItemData();
        ItemData itemData = DataTableManager.ItemTable.Get(id);
        newItem.ItemData = itemData;
        SetItemData(itemData);
    }
}
