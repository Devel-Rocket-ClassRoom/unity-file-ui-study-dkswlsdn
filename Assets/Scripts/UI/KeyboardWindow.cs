using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class KeyboardWindow : GenericWindow
{
    public TextMeshProUGUI typingSpace;
    private string currentString = string.Empty;

    public GameObject keyboard;
    private GameObject[] key = new GameObject[26];

    public Button cancleButton;
    public Button backspaceButton;
    public Button acceptButton;

    private int maxLength = 11;
    private string _underbar = string.Empty;

    private Coroutine blink;
    private const float blinkTime = 0.5f;

    private void Awake()
    {
        int n = 0;
        for (int i = 0; i < keyboard.transform.childCount; i++)
        {
            for (int j = 0; j < keyboard.transform.GetChild(i).childCount; j++)
            {
                key[n] = keyboard.transform.GetChild(i).GetChild(j).gameObject;
                key[n].GetComponent<KeyboardKey>().planeText = this;
                n++;
            }
        }

        cancleButton.onClick.AddListener(Cancle);
        backspaceButton.onClick.AddListener(BackSpace);
        acceptButton.onClick.AddListener(Accept);
    }

    private void Update()
    {
        if (blink == null) blink = StartCoroutine(CoBlinkUnderbar());
    }

    IEnumerator CoBlinkUnderbar()
    {
        yield return new WaitForSeconds(blinkTime);
        if (currentString.Length >= maxLength)
        {
            typingSpace.text = $"{currentString}";
            blink = null;
            yield break;
        }

        _underbar = _underbar == string.Empty ? "_" : string.Empty;
        typingSpace.text = $"{currentString}{_underbar}";
        blink = null;
    }

    public void TypingText(string str)
    {
        if (currentString.Length >= maxLength)
            return;

        currentString = $"{currentString}{str}";
        typingSpace.text = currentString;
    }

    public void BackSpace()
    {
        currentString = currentString.Substring(0, Mathf.Max(0, currentString.Length - 1));
        typingSpace.text = currentString;
    }

    public void Cancle()
    {
        typingSpace.text = string.Empty;
        currentString = string.Empty;
    }

    public void Accept()
    {
        windowManager.Open((int)Window.GameOver);
    }

    public override void Close()
    {
        StopCoroutine(blink);
        blink = null;
        Cancle();
        base.Close();
    }
}
