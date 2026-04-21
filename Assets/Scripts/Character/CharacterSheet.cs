using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CharacterSheet : MonoBehaviour
{
    public int sheetIndex;
    //[HideInInspector]
    public CharacterSummary summary;

    public SaveCharacterData character;

    public Button sheet;
    public Button unlockButton;
    public Button deleteButton;
    public Button protectButton;

    public Image characterIcon;
    public Image weaponIcon;
    public TextMeshProUGUI characterName;

    public ScrollRect InventoryScrollPrefab;
    public GameObject[] gameObjects;


    private void Awake()
    {
        unlockButton.onClick.AddListener(UnLock);
        sheet.onClick.AddListener(SetSummary);
    }

    public void Load()
    {
        Load(character);
    }
    public void Load(SaveCharacterData data)
    {
        if (data == null) return;
        ToggleLock(true);

        character = data;
        characterIcon.sprite = data.CharacterData.SpriteIcon;
        characterName.text = data.CharacterData.StringName;

        if (data.Weapon == null)
        {
            weaponIcon.sprite = null;
        }
        else
        {
            weaponIcon.sprite = data.Weapon.ItemData.SpriteIcon;
        }
    }

    public void SetEmpty()
    {
        ToggleLock(false);
        character = null;
    }

    public void RemoveData()
    {
        summary.SetEmpty(character);
        SaveLoadManager.Data.CharacterDataList.Remove(character);
    }

    public void UnLock()
    {
        ToggleLock(true);

        character = SaveCharacterData.GetRandomCharacter();
        SaveLoadManager.Data.CharacterDataList.Add(character);
        Load();
    }

    public void SetSummary()
    {
        if (character == null) return;
        summary.SetSummary(character);
    }

    void ToggleLock(bool isUnlock)
    {
        unlockButton.gameObject.SetActive(!isUnlock);
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(isUnlock);
        }
    }
}
