using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Mercenary
{
    private MercenaryRole _role;

    private int _level;
    private int _healthPoints;
    private int _maxHealthPoints;

    private string _name;
    private int _price; 
    public enum MercenaryRole
    {
        Hunter,
        Swordsman,
        Priest,
        Mage
    }

    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public void TakeDamage(int damage)
    {
        _healthPoints -= damage;
        EventHandler.onTeamChanged.Invoke();
    }
    
    #region Simple Getter and Setter
    
    public MercenaryRole GetRole()
    {
        return _role;
    }
    
    public void SetRole(MercenaryRole role)
    {
        _role = role;
    }
    
    public int GetLevel()
    {
        return _level;
    }
    
    public void SetLevel(int lvl)
    {
        _level = lvl;
    }
    
    public int GetHealthPoints()
    {
        return _healthPoints;
    }
    
    public void SetHealthPoints(int healthPoints)
    {
        _healthPoints = healthPoints;
    }
    
    public int GetMaxHealthPoints()
    {
        return _maxHealthPoints;
    }
    
    public void SetMaxHealthPoints(int maxHealthPoints)
    {
        _maxHealthPoints = maxHealthPoints;
    }

    public int GetPrice()
    {
        return _price;
    }
    
    public void SetPrice(int price)
    {
        _price = price;
    }
    
    #endregion
    
    public Mercenary(int level = 1)
    {
        _level = level;
        _role = (MercenaryRole) Random.Range(0,3);
        if (_role == MercenaryRole.Swordsman)
        {
            _maxHealthPoints = Random.Range(((100 * level)/2), (200 * level));
        }
        else
        {
            _maxHealthPoints = Random.Range(((100 * level)/4), (100 * level));
        }

        _name = GetRandomName();
        _healthPoints = _maxHealthPoints;
        _price = level * Random.Range(50, 10) + Random.Range(1, 50);
    }

    private string GetRandomName()
    {
        return "Pacolos";
    }
    

}