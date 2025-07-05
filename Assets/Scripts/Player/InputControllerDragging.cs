using Interfaces;
using Settings;
using UnityEngine;
using Zenject;

namespace Player
{
    public class InputControllerDragging : MonoBehaviour
    {
        [Inject] private PlayerMovementCC _playerMovementCC;

        [SerializeField] private Transform _draggingParent;

        private IDraggingItem _currentdraggingItem;

        public bool TryDragObject(RaycastHit raycastHit)
        {
            if (raycastHit.collider.TryGetComponent(out IDraggingItem draggingItem))
            {
                if (_currentdraggingItem == null && draggingItem.TryDrag(_draggingParent))
                {
                    _currentdraggingItem = draggingItem;

                    _playerMovementCC.SetSettings(PlayerMovementSettingsType.Drag);

                    return true;
                }
            }

            return false;
        }

        public bool TryReleaseObject()
        {
            if (_currentdraggingItem != null && _currentdraggingItem.TryRelease())
            {
                _currentdraggingItem = null;

                _playerMovementCC.SetSettings(PlayerMovementSettingsType.Main);

                return true;
            }

            return false;
        }
    }
}
