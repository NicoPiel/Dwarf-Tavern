using System;
using TMPro;
using UnityEngine;

namespace Utility.Tooltip
{
    public class Tooltip : MonoBehaviour
    {
        private static Tooltip _instance;
        
        [SerializeField] private TMP_Text tooltipText;
        [SerializeField] private RectTransform backgroundRectTransform;

        private void Awake()
        {
            _instance = this;
            HideTooltip();
        }
        
        private void ShowTooltip(string tooltipString)
        {
            tooltipText.text = tooltipString;
            var backgroundSize = new Vector2(tooltipText.preferredWidth, tooltipText.preferredHeight);
            backgroundRectTransform.sizeDelta = backgroundSize;
        }
        
        private void ShowTooltip(Tooltip tooltip, string tooltipString)
        {
            tooltip.tooltipText.text = tooltipString;
            var backgroundSize = new Vector2(tooltip.tooltipText.preferredWidth, tooltip.tooltipText.preferredHeight);
            tooltip.backgroundRectTransform.sizeDelta = backgroundSize;
            
            tooltip.gameObject.SetActive(true);
        }

        private void HideTooltip()
        {
            gameObject.SetActive(false);
        }
        
        private void HideTooltip(Tooltip tooltip)
        {
            tooltip.gameObject.SetActive(false);
        }

        public static void ShowTooltip_Static(string tooltipString)
        {
            if (_instance != null) _instance.ShowTooltip(tooltipString);
            else throw new NullReferenceException("Tooltip instance was null.");
        }
        
        public static void ShowTooltip_Static(Tooltip tooltip, string tooltipString)
        {
            if (tooltip != null) tooltip.ShowTooltip(tooltip, tooltipString);
            
            else throw new NullReferenceException("Tooltip instance was null.");
        }

        public static void HideTooltip_Static()
        {
            if (_instance != null) _instance.HideTooltip();
            else throw new NullReferenceException("Tooltip instance was null.");
        }
        
        public static void HideTooltip_Static(Tooltip tooltip)
        {
            if (tooltip != null) tooltip.HideTooltip(tooltip);
            else throw new NullReferenceException("Tooltip instance was null.");
        }
    }
}