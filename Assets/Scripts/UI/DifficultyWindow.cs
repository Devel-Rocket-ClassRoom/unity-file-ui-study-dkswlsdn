using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyWindow : GenericWindow
{
    public Toggle[] toggles;
    public Button cancelButton;
    public Button applyButton;

    public int selected;

    private string ConfigFolderPath => Path.Combine(Application.persistentDataPath, "Config");
    private string ConfigPath => Path.Combine(ConfigFolderPath, "difficulty.txt");

    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);

        cancelButton.onClick.AddListener(Cancel);
        applyButton.onClick.AddListener(Apply);
    }

    public override void Open()
    {
        base.Open();

        //if (!Directory.Exists(ConfigFolderPath))
        //{
        //    Directory.CreateDirectory(ConfigFolderPath);
        //    File.WriteAllText(ConfigPath, selected.ToString());
        //}

        //selected = int.Parse(File.ReadAllText(ConfigPath));

        selected = PlayerPrefs.GetInt("Difficulty");
        toggles[selected].isOn = true;
    }

    public void OnEasy(bool active)
    {
        if (active)
        {
            selected = 0;
            Debug.Log("OnEasy");
        }
    }

    public void OnNormal(bool active)
    {
        if (active)
        {
            selected = 1;
            Debug.Log("OnNormal");
        }
    }

    public void OnHard(bool active)
    {
        if (active)
        {
            selected = 2;
            Debug.Log("OnHard");
        }
    }

    public void Apply()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                // File.WriteAllText(ConfigPath, selected.ToString());
                PlayerPrefs.SetInt("Difficulty", i);
                selected = i;
                break;
            }
        }

        windowManager.Open((int)Window.Title);
    }

    public void Cancel()
    {
        toggles[selected].isOn = true;
        windowManager.Open((int)Window.Title);
    }
}
