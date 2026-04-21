using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : GenericWindow
{
    public Button characterButton;
    public Button inventoryButton;

    private void Awake()
    {
        characterButton.onClick.AddListener(() => windowManager.Open((int)Window.Character));
        inventoryButton.onClick.AddListener(() => windowManager.Open((int)Window.Inventory));
    }
}
