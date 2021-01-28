using UnityEngine;
using Utility.Tooltip;

public class Brewstand : MonoBehaviour
{
    public Tooltip tooltip;

    private static Brewstand _instance;
    
    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
    }

    public static Tooltip GetTooltip()
    {
        return _instance.tooltip;
    }
}
