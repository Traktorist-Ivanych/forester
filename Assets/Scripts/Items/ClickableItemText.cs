using UnityEngine;

namespace Items
{
    public class ClickableItemText : MonoBehaviour, IClickableObjectText
    {
        [SerializeField] private string _clickableText;

        public string ClickableText => _clickableText;
    }
}
