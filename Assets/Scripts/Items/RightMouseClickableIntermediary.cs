using Interfaces;
using UnityEngine;

namespace Items
{
    public class RightMouseClickableIntermediary : MonoBehaviour, IRightMouseClickable
    {
        [SerializeField] private MonoBehaviour _mainRightMouseClickableMonoBehaviour;

        private IRightMouseClickable _mainPickableItem;

        private void Start()
        {
            _mainPickableItem = _mainRightMouseClickableMonoBehaviour as IRightMouseClickable;
        }

        public void OnRightMouseClicked()
        {
            _mainPickableItem.OnRightMouseClicked();
        }
    }
}
