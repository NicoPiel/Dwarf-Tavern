using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class upgrade_tables : Upgradable
{
    [SerializeField] private Sprite lvl1;
    [SerializeField] private Sprite lvl2;
    [SerializeField] private Sprite lvl3;
    private SpriteRenderer _spriteRenderer;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _upgradeStateHolder = UpgradeStateHolder.GetInstance();
        if (!_upgradeStateHolder.IsRegistered(id))
        {
            _upgradeStateHolder.RegisterUpgradeble(id);
            
        }
        Setup();
    }

    void Setup()
    {
        switch (_upgradeStateHolder.GetUpgradeState(id))
        {
            case 1:
                _spriteRenderer.sprite = lvl1;
                break;
            case 2:
                _spriteRenderer.sprite = lvl2;
                break;
            case 3:
                _spriteRenderer.sprite = lvl3;
                break;
        }
    }
}
