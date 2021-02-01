using UnityEngine;

namespace Messages
{
    public class MessageUIElement : MonoBehaviour
    {
        protected Message _msg;

        public void OpenGUI()
        {
            MessageUI.onMessageClick.Invoke(_msg);
        }
    }
}