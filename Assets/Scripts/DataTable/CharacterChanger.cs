using UnityEngine;
using UnityEngine.UI;

public class CharacterChanger : MonoBehaviour
{
    public string characterId;
    public Image Image;
    public Localization text;

    public ShowCharacterDescription CharacterDesc;

    public void OnClick()
    {
        CharacterDesc.SetCharacterData(characterId);
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
        CharacterData data = DataTableManager.CharacterTable.Get(characterId ?? "Unknown");
        Image.sprite = data.SpriteIcon;
        text.ChangeText(data.Name);
    }

    public void ChangeIcon(string id) 
    {
        characterId = id;
        OnChangeItemId();
    }
}
