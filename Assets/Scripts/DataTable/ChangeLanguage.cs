using UnityEngine;

public class ChangeLanguage : MonoBehaviour
{
    [SerializeField]
    private Languages languages;

    public void OnClick()
    {
        Variables.Languages = languages;
    }
}
