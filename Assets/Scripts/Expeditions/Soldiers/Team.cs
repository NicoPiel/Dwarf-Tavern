using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    private Mercenary _teamSlot1;
    private Mercenary _teamSlot2;
    private Mercenary _teamSlot3;
    
    public void AddToTeam(Mercenary mercenary)
    {
        if (_teamSlot1 == null)
        {
            _teamSlot1 = mercenary;
        }
        else if(_teamSlot2 == null)
        {
            _teamSlot2 = mercenary;
        }
        else if(_teamSlot3 == null)
        {
            _teamSlot3 = mercenary;
        }
        else
        {
            return;
        }
        EventHandler.onTeamChanged.Invoke();
    }
    public void RemoveFromTeam(Mercenary mercenary)
    {
        if (_teamSlot1 == mercenary)
        {
            _teamSlot1 = null;
        }
        if (_teamSlot2 == mercenary)
        {
            _teamSlot2 = null;
        }
        if (_teamSlot3 == mercenary)
        {
            _teamSlot3 = null;
        }
        EventHandler.onTeamChanged.Invoke();
    }
    private int FindMercenaryIdInTeam(Mercenary mercenary)
    {
        if (_teamSlot1.Equals(mercenary))
        {
            return 1;
        }
        if (_teamSlot2.Equals(mercenary))
        {
            return 2;
        }
        if (_teamSlot3.Equals(mercenary))
        {
            return 3;
        }

        return 0;

    }
    public Mercenary GetTeamMember(int id)
    {
        switch (id)
        {
            case 1:
                return _teamSlot1;
            case 2:
                return _teamSlot2;
            case 3:
                return _teamSlot3;
        }

        return null;
    }
    public bool IsSomeoneInTeam()
    {
        if (_teamSlot1 != null || _teamSlot2 != null || _teamSlot3 != null)
        {
            return true;
        }
        return false;
    }
    public Mercenary GetRandomTeamMember(int randomNumber = 0)
    {
        if (!IsSomeoneInTeam())
        {
            return null;
        }
        if(randomNumber == 0)
            randomNumber = Random.Range(1, 4);
        
        if (randomNumber == 1)
        {
            if (_teamSlot1 != null)
            {
                return _teamSlot1;
            }

            randomNumber = 2;
        }

        if (randomNumber == 2)
        {
            if (_teamSlot2 != null)
            {
                return _teamSlot2;
            }

            randomNumber = 2;
        }

        if (randomNumber == 3)
        {
            if (_teamSlot3 != null)
            {
                return _teamSlot3;
            }
            randomNumber = 1;
        }
        GetRandomTeamMember(randomNumber);
        return null;
    }

    public bool IsFull()
    {
        return _teamSlot1 != null && _teamSlot2 != null && _teamSlot3 != null;
    }
}
