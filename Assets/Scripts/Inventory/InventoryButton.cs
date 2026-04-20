using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [HideInInspector]
    public ItemDescription ItemDescription;
    public Image icon;
    public TextMeshProUGUI itemName;
    public SaveItemData item { get; private set; }


    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SelectItem);
    }

    public void SetEmpty()
    {
        item = null;
        itemName.text = string.Empty;
        icon.sprite = null;
    }

    public void SetItem(SaveItemData data)
    {
        if (data == null) return;

        item = data;
        icon.sprite = data.ItemData.SpriteIcon;
        itemName.text = data.ItemData.StringName;
    }

    private void SelectItem()
    {
        if (ItemDescription == null || item == null) return;

        ItemDescription.SetItemData(item);
    }
}
