using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expedition
{
    private string _name;
    private int _difficulty;
    
    private List<Mercenary> _team;
    private int _length;
    private int _karma;

    public Expedition(int difficulty)
    {
        _difficulty = difficulty;
        _karma = 1;
        _length = Random.Range(1, _difficulty * 2);
        _team = new List<Mercenary>();
    }

    #region Simple Getter/Setter
    public int GetKarma()
    {
        return _karma;
    }
    public void SetKarma(int karma)
    {
        _karma = karma;
    }
    public void AddKarma(int karma)
    {
        _karma += karma;
    }
    public string GetName()
    {
        return _name;
    }
    public void SetName(string name)
    {
        _name = name;
    }
    public int GetDifficulty()
    {
        return _difficulty;
    }
    public void SetDifficulty(int difficulty)
    {
        _difficulty = difficulty;
    }
    public void AddToTeam(Mercenary mercenary)
    {
        if(_team.Count <= 3)
            _team.Add(mercenary);
    }
    public void RemoveFromTeam(Mercenary mercenary)
    {
        _team.Remove(mercenary);
    }
    #endregion
    
    public void DamageRandom()
    {
        int random = Random.Range(0, _team.Count + 1);
        _team[random].TakeDamage(Random.Range(1, 100));

        if (_team[random].GetHealthPoints() == 0)
        {
            RemoveFromTeam(_team[random]);
        }
    }
    public void KillRandom()
    {
        int random = Random.Range(0, _team.Count + 1);
        _team[random].SetHealthPoints(0);
        if (_team[random].GetHealthPoints() == 0)
        {
            RemoveFromTeam(_team[random]);
        }
    }
    
    public enum Rewards
    {
        LootDrop,
        MoneyDrop,
    }
    public enum Loss
    {
        Damage,
        Death
    }

    
    
}