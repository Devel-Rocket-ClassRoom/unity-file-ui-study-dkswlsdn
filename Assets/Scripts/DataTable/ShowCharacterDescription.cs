using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowCharacterDescription : MonoBehaviour
{
    public Image image;
    public Localization cName;
    public Localization desc;
    public Localization attack;
    public Localization defense;

    public void OnClick()
    {
        
    }

    public void SetCharacterData(CharacterData data)
    {
        if (data == null) return;

        image.sprite = data.SpriteIcon;
        cName.ChangeText(data.Name);
        desc.ChangeText(data.Desc);
        attack.ChangeText("Attack", data.Attack);
        defense.ChangeText("Defense", data.Defense);
    }

    public void SetCharacterData(string id)
    {
        CharacterData characterData = DataTableManager.CharacterTable.Get(id);
        SetCharacterData(characterData);
    }
}
