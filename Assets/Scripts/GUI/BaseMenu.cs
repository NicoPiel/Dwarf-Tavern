using UnityEngine;

public abstract class BaseMenu : MonoBehaviour
{
    protected bool isShown = false;

    public abstract void ShowMenu();

    public abstract void HideMenu();

    public bool IsShown()
    {
        return isShown;
    }
}
