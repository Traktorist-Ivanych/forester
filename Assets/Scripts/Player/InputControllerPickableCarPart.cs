using Interfaces;
using UI;
using UnityEngine;
using Zenject;

namespace Player
{
    public class InputControllerPickableCarPart : MonoBehaviour
    {
        [Inject] private HUDCanvas _hudCanvas;

        private IPickableItemCarPart _currentCarPart;

        private void Update()
        {
            if (_currentCarPart != null && _currentCarPart.CanBeInstalled)
            {
                _hudCanvas.SetCheckMarkEnabled(true);
            }
            else
            {
                _hudCanvas.SetCheckMarkEnabled(false);
            }
        }

        public bool TryPickupCarPart(IPickableItem pickableItem)
        {
            if (_currentCarPart == null && pickableItem is IPickableItemCarPart)
            {
                _currentCarPart = pickableItem as IPickableItemCarPart;

                return true;
            }

            return false;
        }

        public bool TryInstallCarPart()
        {
            if (_currentCarPart == null) return false;

            if (_currentCarPart.CanBeInstalled)
            {
                _currentCarPart.Install();
                _currentCarPart = null;

                return true;
            }

            return false;
        }

        public void ReleaseCarPart()
        {
            _currentCarPart = null;
        }
    }
}
