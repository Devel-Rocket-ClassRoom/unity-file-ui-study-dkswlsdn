using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowCharacterDescription : MonoBehaviour
{
    public Image image;
    public Localization cName;
    public Localization desc;
    public TextMeshProUGUI attack;
    public TextMeshProUGUI defense;

    public void OnClick()
    {
        
    }

    public void SetCharacterData(CharacterData data)
    {
        if (data == null) return;

        image.sprite = data.SpriteIcon;
        cName.ChangeText(data.Name);
        desc.ChangeText(data.Desc);
        attack.text = data.Attack;
        defense.text = data.Defense;
    }

    public void SetCharacterData(string id)
    {
        CharacterData characterData = DataTableManager.CharacterTable.Get(id);
        SetCharacterData(characterData);
    }
}
