using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconChanger : MonoBehaviour
{
    public string itemId;
    public Image Image;
    public Localization text;

    public ShowItemDescription IconDesc;

    public void OnClick()
    {
        IconDesc.SetItemData(itemId);
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            OnChangeItemId();
        }
    }

    private void OnChangeItemId()
    {
        ItemData data = DataTableManager.ItemTable.Get(itemId ?? "Unknown");
        Image.sprite = data.SpriteIcon;
        text.message = data != null ? data.Name : "Unknoun";

        if (text != null)
        {
            text.ChangeText(text.message);
        }
    }

    public void ChangeIcon(string id)
    {
        itemId = id;
        OnChangeItemId();
    }
}
