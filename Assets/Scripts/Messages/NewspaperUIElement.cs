using TMPro;
using UnityEngine;

namespace Messages
{
    public class NewspaperUIElement : MessageUIElement
    {
        public TextMeshProUGUI firstHeadline;
        public void SetMessage(NewspaperMessage msg)
        {
            _msg = msg;
            firstHeadline.text = msg.articles[0].headline;
        }
    }
}