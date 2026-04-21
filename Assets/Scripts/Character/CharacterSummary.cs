using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSummary : MonoBehaviour
{
    private SaveCharacterData characterData;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI summaryAttack;
    public TextMeshProUGUI summaryDefense;

    public Image weaponIcon;
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponValue;

    public void SetSummary(SaveCharacterData data)
    {
        characterData = data;

        characterIcon.sprite = data.CharacterData.SpriteIcon;
        characterName.text = data.CharacterData.StringName;

        int sumAtk = int.Parse(data.CharacterData.Attack);
        int sumDef = int.Parse(data.CharacterData.Defense);

        if (data.Weapon != null)
        {
            int sum = data.Weapon.ItemData.Value;
            string type = string.Empty;

            if (data.Weapon.ItemData.Type == ItemTypes.Weapon)
            {
                sumAtk += sum;
                type = "Attack";
            }
            else if (data.Weapon.ItemData.Type == ItemTypes.Equip)
            {
                sumDef += sum;
                type = "Defense";
            }

            weaponIcon.sprite = data.Weapon.ItemData.SpriteIcon;
            weaponValue.text = $"{DataTableManager.StringTable.Get(type)} : {sum}";
        }

        summaryAttack.text = $"{DataTableManager.StringTable.Get("Attack")} : {sumAtk.ToString()}";
        summaryDefense.text = $"{DataTableManager.StringTable.Get("Defense")} : {sumDef.ToString()}";
    }

    public void SetEmpty(SaveCharacterData data)
    {
        if (data != characterData) return;

        SetEmpty();
    }

    public void SetEmpty()
    {
        characterIcon.sprite = null;
        characterName.text = string.Empty;
        summaryAttack.text = string.Empty;
        summaryDefense.text = string.Empty;
        weaponIcon.sprite = null;
        weaponValue.text = string.Empty;
    }
}
