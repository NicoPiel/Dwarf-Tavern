using System;
using System.Collections;
using System.Collections.Generic;
using Simulation.Core;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Expedition
{
    private string _name;
    private int _difficulty;
    private ThemeType _theme;
    private Team _team;
    private bool _started;
    

    private int _length;
    private int _karma;
    private int _completedEvents;
    

    public Expedition(int difficulty)
    {
        _difficulty = difficulty;
        _karma = 1;
        _length = Random.Range(1, _difficulty * 2);
        _team = new Team();
        _theme = (ThemeType) Random.Range(1,4);
        _name = GameManager.GetRandomPlace(_theme.ToString().ToLower());
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

    public int GetLength()
    {
        return _length;
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

    public ThemeType GetThemeType()
    {
        return _theme;
    }

    public void setThemeType(ThemeType theme)
    {
        _theme = theme;
    }

    public void StartExpedition()
    {
        _started = true;
        EventHandler.onExpeditionHolderChanged.Invoke();
        EventHandler.onExpeditionStarted.Invoke();
    }

    public bool IsStarted()
    {
        return _started;
    }

    public int GetCompletedEvents()
    {
        return _completedEvents;
    }

    public void SetCompletedEvents(int count)
    {
        _completedEvents = count;
    }

    public void AddCompletedEvents(int count)
    {
        _completedEvents += count;
    }
    #endregion
    
    public void DamageRandom()
    {
        var mercenary = _team.GetRandomTeamMember();
        if(mercenary == null)
            return;
        
        mercenary.TakeDamage(Random.Range(1, 100));
        
        if (mercenary.GetHealthPoints() == 0)
        {
            _team.RemoveFromTeam(mercenary);
        }
    }
    public void KillRandom()
    {
        var mercenary = _team.GetRandomTeamMember();
        if (mercenary == null)
            return;
        mercenary.SetHealthPoints(0);

        if (mercenary.GetHealthPoints() == 0)
        {
            _team.RemoveFromTeam(mercenary);
        }
    }

    public Team GetTeam()
    {
        return _team;
    }
    
    
    public enum ThemeType
    {
        Default,
        Jungle,
        Cave,
        Forest,
    }

    
    
}