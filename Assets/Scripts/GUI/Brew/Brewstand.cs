using UnityEngine;
using Utility.Tooltip;

public class Brewstand : MonoBehaviour
{
    public Tooltip tooltip;
    public AudioSource audioSource;

    public AudioClip onBrewBeginDragSound;
    public AudioClip onBrewEndDragSound;
    public AudioClip onBrewButtonClicked;


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

    public static Brewstand GetInstance()
    {
        return _instance;
    }
}
