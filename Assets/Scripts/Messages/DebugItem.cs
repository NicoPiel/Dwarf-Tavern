using TMPro;
using UnityEngine;

namespace Messages
{
    public class DebugItem : MonoBehaviour
    {
        public TextMeshProUGUI idField;
        public TextMeshProUGUI taskField;
        private Message _msg;
        public void Fill(Message m)
        {
            _msg = m;
            idField.text = m.name;
        }

        public void Cancel()
        {
            
        }
    }
}