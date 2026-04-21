using UnityEngine;
using UnityEngine.UI;

public class OpenMainMenu : GenericWindow
{
    public Button openMenuButton;

    private void Awake()
    {
        openMenuButton.onClick.AddListener(Open);
    }

    private new void Open()
    {
        windowManager.Open((int)Window.MainMenu);
    }
}
