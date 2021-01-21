using UnityEngine;

public abstract class BaseMenu : MonoBehaviour
{
    protected bool isShown = false;

    public virtual void ShowMenu()
    {
        isShown = true;
    }

    public virtual void HideMenu()
    {
        isShown = false;
    }

    public bool IsShown()
    {
        return isShown;
    }
}
