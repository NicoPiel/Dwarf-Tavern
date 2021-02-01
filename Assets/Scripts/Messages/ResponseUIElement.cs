using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Messages
{
    public class ResponseUIElement : MonoBehaviour
    {
        private LetterUI _letterUI;
        public TextMeshProUGUI responseText;
        private LetterMessage.ResponseOption _response;

        public void SetResponse(LetterMessage.ResponseOption response, LetterUI letterUI)
        {
            _response = response;
            _letterUI = letterUI;
            responseText.text = response.displayText;
        }

        public void SetClickable(bool clickable)
        {
            GetComponent<Button>().interactable = clickable;
        }

        public void Respond()
        {
            _letterUI.Respond(_response);
        }
    }
}