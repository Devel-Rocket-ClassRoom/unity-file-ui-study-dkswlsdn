using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GenericWindow[] windows;
    [HideInInspector]
    public static Stack<GenericWindow> WindowStack = new Stack<GenericWindow>();

    public int currentWindowId;
    public int defaultWindowId;


    private void Awake()
    {
        foreach (var window in windows)
        {
            if (window == null) continue;
            window.gameObject.SetActive(false);
            window.Init(this);
        }

        currentWindowId = defaultWindowId;
        windows[defaultWindowId].gameObject.SetActive(true);
    }

    public GenericWindow Open(int id)
    {
        windows[currentWindowId].Close();
        currentWindowId = id;
        windows[currentWindowId].Open();

        return windows[currentWindowId];
    }
}

public enum Window
{
    OpenMenu, Title, GameOver, NewGame, Difficulty, MainMenu, Character, Inventory,
}