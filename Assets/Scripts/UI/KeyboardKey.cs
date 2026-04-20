using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardKey : MonoBehaviour
{
    [HideInInspector]
    public KeyboardWindow planeText;
    private Button key;
    private string str;

    private void Awake()
    {
        key = GetComponent<Button>();
        str = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        key.onClick.AddListener(TypingText);
    }

    private void TypingText()
    {
        planeText.TypingText(str);
    }
}
