using System;
using TMPro;
using UnityEngine;

namespace Messages
{
    public class LetterMessageUIElement : MessageUIElement
    {
        public TextMeshProUGUI senderText;
        public void SetMessage(LetterMessage msg)
        {
            _msg = msg;
            senderText.text = msg.sender;
        }
        
    }
}