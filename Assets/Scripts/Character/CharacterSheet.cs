using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CharacterSheet : MonoBehaviour
{
    public int sheetIndex;
    [HideInInspector]
    public Transform contentTransform;

    public SaveCharacterData character;

    public CharacterSheet sheet;
    public Button unlockButton;
    public Button weaponButton;
    public Button deleteButton;
    public Button protectButton;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI characterAttack;
    public TextMeshProUGUI characterDefense;
    public TextMeshProUGUI WeaponName;
    public TextMeshProUGUI WeaponAttack;
    public TextMeshProUGUI WeaponDefense;

    public ScrollRect InventoryScrollPrefab;
    public GameObject[] gameObjects;


    private void Awake()
    {
        unlockButton.onClick.AddListener(UnLock);
        weaponButton.onClick.AddListener(OpenInventory);
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

        if (data.Weapon == null) return;
        weaponButton.GetComponent<Image>().sprite = data.Weapon.ItemData.SpriteIcon;
        WeaponName.text = data.Weapon.ItemData.StringName ?? "무기 없음";
        
    }

    public void SetEmpty()
    {
        ToggleLock(false);
        character = null;
    }

    public void RemoveData()
    {
        SaveLoadManager.Data.CharacterDataList.Remove(character);
    }

    public void UnLock()
    {
        ToggleLock(true);

        character = SaveCharacterData.GetRandomCharacter();
        SaveLoadManager.Data.CharacterDataList.Add(character);
        Load();
    }

    public void OpenInventory()
    {

    }

    public void OpenCharacterSheet()
    {
        if (contentTransform.childCount < sheetIndex + 1)
        {

        }
        else
        {
            contentTransform.GetChild(sheetIndex + 1).gameObject.SetActive(true);
        }

        Load();
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
