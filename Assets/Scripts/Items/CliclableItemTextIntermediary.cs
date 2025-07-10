using UnityEngine;

namespace Items
{
    public class CliclableItemTextIntermediary : MonoBehaviour, IClickableObjectText
    {
        [SerializeField] private MonoBehaviour _clickableObjectTextMonoBehavior;

        private IClickableObjectText _mainClickableObjectText;

        public string ClickableText => _mainClickableObjectText.ClickableText;

        void Start()
        {
            _mainClickableObjectText = _clickableObjectTextMonoBehavior as IClickableObjectText;
        }
    }
}
