using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgradable : MonoBehaviour
{
    public string id;
    [SerializeField] private Sprite lvl1;
    [SerializeField] private Sprite lvl2;
    [SerializeField] private Sprite lvl3;

    private UpgradeStateHolder _upgradeStateHolder;
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    
    private void Setup()
    {
        // Debug.Log("Starting Setup with id"+ id + _upgradeStateHolder.GetUpgradeState(id));
        switch (_upgradeStateHolder.GetUpgradeState(id))
        {
            case 1:
                // Debug.Log("Should be lvl 1 now");
                _spriteRenderer.sprite = lvl1;
                break;
            case 2:
                // Debug.Log("Should be lvl 2 now");
                _spriteRenderer.sprite = lvl2;
                break;
            case 3:
                // Debug.Log("Should be lvl 3 now");
                _spriteRenderer.sprite = lvl3;
                break;
        }
    }
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _upgradeStateHolder = UpgradeStateHolder.GetInstance();
        if (!_upgradeStateHolder.IsRegistered(id))
        {
            _upgradeStateHolder.RegisterUpgradeble(id);
            Debug.Log("Registered");
        }
        Setup();
    }
}
