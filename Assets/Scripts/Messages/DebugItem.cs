using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Messages
{
    public class DebugItem : MonoBehaviour
    {
        public TextMeshProUGUI idField;
        public TextMeshProUGUI taskField;
        private Message _msg;
        private MessageTask _task;
        public void Fill(Message m, MessageTask task)
        {
            _msg = m;
            _task = task;
            idField.text = m.name;
            taskField.text = task.name;
            UpdateColor();
        }

        public void Toggle()
        {
            if (MessageSystemHandler.Instance.IsCancelled(_task))
            {
                Resume();
            }
            else
            {
                Cancel();
            }
        }

        public void Cancel()
        {
            MessageSystemHandler.Instance.Cancel(_task);
            UpdateColor();
        }

        public void Resume()
        {
            MessageSystemHandler.Instance.Resume(_task);
            UpdateColor();
        }

        private void UpdateColor()
        {
            if (MessageSystemHandler.Instance.IsCancelled(_task))
            {
                GetComponent<Image>().color = Color.red;
            }
            else
            {
                GetComponent<Image>().color = Color.green;
            }
        }
    }
}