using UnityEngine;
using UnityEngine.EventSystems;

public class GenericWindow : MonoBehaviour
{
    public GameObject firstSelected;

    private WindowManager windowManager;
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
}
