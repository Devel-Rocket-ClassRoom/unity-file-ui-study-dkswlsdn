using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GenericWindow : MonoBehaviour
{
    public GameObject firstSelected;
    public Button closeButton;
    public Button backButton;
    protected WindowManager windowManager;

    private void Awake()
    {
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(Close);
        }
    }

    public void Init(WindowManager mgr)
    {
        windowManager = mgr;
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public virtual void Close()
    {

        gameObject.SetActive(false);
    }

    public virtual void Back()
    {
        
    }
}
