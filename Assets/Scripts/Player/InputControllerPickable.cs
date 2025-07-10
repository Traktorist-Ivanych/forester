using Interfaces;
using UnityEngine;

namespace Player
{
    public class InputControllerPickable : MonoBehaviour
    {
        [SerializeField] private Transform _originRaycastTransform;
        [SerializeField] private Transform _handsTransform;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _trowSpeed;

        [SerializeField] private InputControllerPickableCarPart _inputControllerPickableCarPart;

        private IPickableItem _currentPickableItem;

        public bool TryPickupObject(RaycastHit raycastHit)
        {
            if (_currentPickableItem != null) return false;

            else if (raycastHit.collider.TryGetComponent(out IPickableItem pickableItem))
            {
                _currentPickableItem = pickableItem.OriginalIPickableItem;
                _currentPickableItem.Pickup(_handsTransform);

                _inputControllerPickableCarPart.TryPickupCarPart(pickableItem);

                return true;
            }

            return false;
        }

        public bool TryReleaseObject()
        {
            if (_currentPickableItem == null) return false;

            if (_inputControllerPickableCarPart.TryInstallCarPart())
            {
                _currentPickableItem = null;

                return true;
            }
            else if (CanRelease())
            {
                if (_currentPickableItem.TryRelease())
                {
                    _currentPickableItem = null;
                    _inputControllerPickableCarPart.ReleaseCarPart();

                    return true;
                }
            }

            return false;
        }

        public bool TryThrowObject()
        {
            if (CanRelease())
            {
                Vector3 force = _originRaycastTransform.forward * _trowSpeed;

                if (_currentPickableItem.TryThrow(force))
                {
                    _currentPickableItem = null;
                    _inputControllerPickableCarPart.ReleaseCarPart();

                    return true;
                }
            }

            return false;
        }

        public void RotatePickableItem(Vector2 input)
        {
            if (_currentPickableItem != null && input.y != 0)
            {
                _currentPickableItem.ItemTransform.Rotate(_originRaycastTransform.right, _rotationSpeed * input.y, Space.World);
            }
        }

        private bool CanRelease()
        {
            Vector3 direction = _currentPickableItem.ItemTransform.position - _originRaycastTransform.position;

            if (Physics.Raycast(_originRaycastTransform.position, direction, out RaycastHit hit))
            {
                if (hit.collider.transform.IsChildOf((_currentPickableItem as MonoBehaviour).transform))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
