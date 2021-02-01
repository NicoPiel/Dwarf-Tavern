using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using TMPro;
using UnityEngine;

namespace Messages
{
    public class LetterUI : MonoBehaviour
    {
        public GameObject responseList;
        public GameObject responsePrefab;
        public TextMeshProUGUI text;
        private LetterMessage _msg;
        
        public void SetMessage(LetterMessage msg)
        {
            Clear();
            _msg = msg;
            text.text = msg.text;
            AddOptions();
        }

        public void Respond(LetterMessage.ResponseOption response)
        {
            EventHandler.onLetterResponse.Invoke(new KeyValuePair<LetterMessage, LetterMessage.ResponseOption>(_msg, response));
            MessageSystemHandler.Instance.SetResponded(_msg, true);
            UpdateOptions(false);
        }

        private void Clear()
        {
            foreach (var elem in responseList.GetComponentsInChildren<ResponseUIElement>())
            {
                Destroy(elem.gameObject);
            }
        }

        private void AddOptions()
        {
            bool clickable = !MessageSystemHandler.Instance.IsRespondedTo(_msg);
            foreach (var option in _msg.responseOptions)
            {
                GameObject listEntry = Instantiate(responsePrefab, responseList.transform);
                ResponseUIElement element = listEntry.GetComponent<ResponseUIElement>();
                element.SetResponse(option, this);
                element.SetClickable(clickable);
            }
        }

        private void UpdateOptions(bool clickable)
        {
            foreach (var elem in responseList.GetComponentsInChildren<ResponseUIElement>())
            {
                elem.SetClickable(clickable);
            }
        }
    }
}