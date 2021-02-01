using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Messages
{
    public class MessageUI : MonoBehaviour
    {
        public static UnityEvent<Message> onMessageClick;

        public GameObject letterPrefab;
        public GameObject newsPrefab;
        public GameObject listView;
        public LetterUI letterUI;
        public NewspaperUI newsUI;
        
        private int lastDayCalled = 0;
        void OnEnable()
        {
            int day = DayCounter.GetInstance().GetDayCount();
            if (lastDayCalled != day)
            {
                onMessageClick = new UnityEvent<Message>();
                onMessageClick.AddListener(OpenMessage);
                Clear();
                FillDay(day);
                lastDayCalled = day;
            }
        }

        public void Clear()
        {
            foreach (var elem in listView.GetComponentsInChildren<MessageUIElement>())
            {
                Destroy(elem.gameObject);
            }
        }

        public void FillDay(int day)
        {
            foreach (var message in MessageSystemHandler.Instance.GetMessagesForDay(day).Select(entry => entry.Value))
            {
                if (message is LetterMessage)
                {
                    GameObject listEntry = Instantiate(letterPrefab, listView.transform);
                    LetterMessageUIElement uiElement = listEntry.GetComponent<LetterMessageUIElement>();
                    uiElement.SetMessage((LetterMessage) message);
                }else if (message is NewspaperMessage)
                {
                    GameObject listEntry = Instantiate(newsPrefab, listView.transform);
                    NewspaperUIElement uiElement = listEntry.GetComponent<NewspaperUIElement>();
                    uiElement.SetMessage((NewspaperMessage) message);
                }   
            }
        }

        public void OpenMessage(Message msg)
        {
            if (msg is LetterMessage)
            {
                letterUI.SetMessage((LetterMessage) msg);
                letterUI.gameObject.SetActive(true);
            }else if (msg is NewspaperMessage)
            {
                newsUI.SetNewspaper((NewspaperMessage) msg);
                newsUI.gameObject.SetActive(true);
            }
        }
    }
}
