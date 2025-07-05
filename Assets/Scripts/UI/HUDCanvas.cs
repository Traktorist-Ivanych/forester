using TMPro;
using UnityEngine;

namespace UI
{
    public class HUDCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _clickableObjectsText;

        private void Awake()
        {
            _clickableObjectsText.enabled = false;
        }

        public void ShowClickableObjectsText(string text)
        {
            _clickableObjectsText.enabled = true;
            _clickableObjectsText.text = text;
        }

        public void HideClickableObjectsText()
        {
            _clickableObjectsText.enabled = false;
        }
    }
}
