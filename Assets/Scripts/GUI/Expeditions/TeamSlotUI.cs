using System.Collections;
using System.Collections.Generic;
using Inventory;
using TMPro;
using UnityEngine;

public class TeamSlotUI : MonoBehaviour
{
    public int teamSlotID;
    public TMP_Text role;

    public TMP_Text hp;
    
    // Start is called before the first frame update
    void Start()
    {
        EventHandler.onTeamChanged.AddListener(UpdateUI);
        EventHandler.onExpeditionStarted.AddListener(UpdateUI);
        UpdateUI();
    }

    // Update is called once per frame
    private void UpdateUI()
    {
        if (!ExpeditionHolder.GetInstance().IsExpeditionSelected())
        {
            gameObject.transform.localScale = Vector3.zero;
            return;
        }
        gameObject.transform.localScale = Vector3.one;
        
        var expo = ExpeditionHolder.GetInstance().GetSelectedExpedition();
        var mercenary = expo?.GetTeam().GetTeamMember(teamSlotID);
        if (mercenary != null)
        {
            gameObject.transform.localScale = Vector3.one;
            role.text = mercenary.GetRole().ToString();
            hp.text = $"HP: {mercenary.GetHealthPoints()}/{mercenary.GetMaxHealthPoints()}";
        }
        else
        {
            gameObject.transform.localScale = Vector3.zero;
            role.text = "";
            hp.text = "";
        }
    }

    public void Dismiss()
    {
        Team team = ExpeditionHolder.GetInstance().GetSelectedExpedition().GetTeam();
        var m = team.GetTeamMember(teamSlotID);
        InventoryManager.GetInstance().GetPlayerInventory().AddFunds(m.GetPrice());
        team.RemoveFromTeam(m);
    }
}
