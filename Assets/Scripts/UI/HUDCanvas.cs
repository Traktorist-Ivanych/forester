using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUDCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _clickableObjectsText;
        [SerializeField] private Image _ckeckMark;

        private void Awake()
        {
            _clickableObjectsText.enabled = false;
        }

        public void SetCheckMarkEnabled(bool isEnabled)
        {
            _ckeckMark.enabled = isEnabled;
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
