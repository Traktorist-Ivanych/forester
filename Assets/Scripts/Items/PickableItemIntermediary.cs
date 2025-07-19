using Interfaces;
using UnityEngine;

namespace Items
{
    public class PickableItemIntermediary : MonoBehaviour, IPickableItem
    {
        [SerializeField] private MonoBehaviour _mainPickableItemMonoBehaviour;

        private IPickableItem _mainPickableItem;

        public IPickableItem OriginalIPickableItem => _mainPickableItem;

        public Transform ItemTransform => _mainPickableItem.ItemTransform;

        private void OnValidate()
        {
            if (_mainPickableItemMonoBehaviour != null) return;

            _mainPickableItemMonoBehaviour = GetComponentInParent<IPickableItem>() as MonoBehaviour;

            while (_mainPickableItemMonoBehaviour is PickableItemIntermediary)
            {
                if (_mainPickableItemMonoBehaviour.transform.parent == null) break;

                _mainPickableItemMonoBehaviour = _mainPickableItemMonoBehaviour.transform.parent.gameObject.GetComponentInParent<IPickableItem>() as MonoBehaviour;
            }
        }

        private void Start()
        {
            _mainPickableItem = _mainPickableItemMonoBehaviour as IPickableItem;
        }

        public void Pickup(Transform parent)
        {
            _mainPickableItem.Pickup(parent);
        }

        public bool TryRelease()
        {
            Debug.Log("TryRelease Wrenches PickableItemIntermediary");
            return _mainPickableItem.TryRelease();
        }

        public bool TryThrow(Vector3 force)
        {
            return _mainPickableItem.TryThrow(force);
        }
    }
}
