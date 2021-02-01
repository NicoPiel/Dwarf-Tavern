using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Messages
{
    public class NewspaperUI : MonoBehaviour
    {
        public TextMeshProUGUI headline;
        public TextMeshProUGUI article;
        public TextMeshProUGUI page;
        public Button next;
        public Button prev;
        private int _page = 0;
        private NewspaperMessage _msg;

        public void SetNewspaper(NewspaperMessage msg)
        {
            _msg = msg;
            _page = 0;
            UpdateTexts();
        }
        
        public void UpdateTexts()
        {
            headline.text = _msg.articles[_page].headline;
            article.text = _msg.articles[_page].articleText;
            page.text = $"Artikel {_page + 1}/{_msg.articles.Length}";
            prev.interactable = true;
            next.interactable = true;
            if (_page <= 0)
            {
                prev.interactable = false;
            }

            if (_page >= _msg.articles.Length - 1)
            {
                next.interactable = false;
            }
        }

        public void Next()
        {
            _page++;
            UpdateTexts();
        }

        public void Prev()
        {
            _page--;
            UpdateTexts();
        }

    }
}