using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mercenary : MonoBehaviour
{
    public MercenaryRole role;

    private int _healthPoints;
    private int _maxHealthPoints;

    public enum MercenaryRole
    {
        
        Hunter,
        Swordsman,
        Priest,
        Mage
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}