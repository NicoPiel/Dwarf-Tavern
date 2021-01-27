using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

namespace Messages
{
    public class DebugUI : MonoBehaviour
    {
        public TMP_InputField inputField;
        public GameObject gridView;
        public GameObject prefab;

        public void Fill()
        {
            foreach (var component in gridView.GetComponentsInChildren<DebugItem>())
            {
                Destroy(component.gameObject);
            }

            int day = int.Parse(inputField.text);
            Dictionary<MessageTask,Message> messages = MessageSystemHandler.Instance.GetMessagesForDay(day);
            foreach (var message in messages)
            {
                GameObject item = Instantiate(prefab, gridView.transform);
                item.GetComponent<DebugItem>().Fill(message.Value, message.Key);
            }
        }
        
    }
}