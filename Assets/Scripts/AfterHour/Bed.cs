using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Messages;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bed : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnAction()
    {
        if (checkSleepRequirement())
        {
            EventHandler.onDayChanged.Invoke();
            SceneManager.LoadScene("Scenes/Tavern");
        }
    }

    public bool checkSleepRequirement()
    {
        MessageSystemHandler msgSystem = MessageSystemHandler.Instance;
        foreach (var letterMessage in msgSystem.GetMessagesForDay(DayCounter.GetInstance().GetDayCount()).Select(entry => entry.Value).OfType<LetterMessage>())
        {
            if (letterMessage.responseOptions.Length > 0 && !msgSystem.IsRespondedTo(letterMessage))
            {
                return false;
            }
        }

        return true;
    }
}
