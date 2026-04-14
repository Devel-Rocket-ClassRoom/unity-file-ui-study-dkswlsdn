using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ShowItemDescription : MonoBehaviour
{
    public Image image;
    public Localization text;
    public Localization desc;

    public void OnClick()
    {
        Debug.Log($"{text.message} ¿Â∫Òµ ");
    }

    public void SetItemData(ItemData data)
    {
        if (data == null) return;

        image.sprite = data.SpriteIcon;
        text.ChangeText(data.Name);
        desc.ChangeText(data.Desc);
    }
    public void SetItemData(string id)
    {
        ItemData itemData = DataTableManager.ItemTable.Get(id);
        SetItemData(itemData);
    }
}
