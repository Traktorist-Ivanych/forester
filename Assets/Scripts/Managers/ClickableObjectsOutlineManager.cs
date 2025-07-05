using Player;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class ClickableObjectsOutlineManager : MonoBehaviour
    {
        [Inject] private readonly PlayerInputManager _playerRaycast;

        private bool _isOutlineEnabled;

        private void OnEnable()
        {
            _playerRaycast.RaycastStartAction += OnPlayerRaycastStarted;
            _playerRaycast.RaycastFinishAction += OnPlayerRaycastFinished;
        }

        private void OnDisable()
        {
            _playerRaycast.RaycastStartAction -= OnPlayerRaycastStarted;
            _playerRaycast.RaycastFinishAction -= OnPlayerRaycastFinished;
        }

        private void OnPlayerRaycastStarted(RaycastHit hitInfo)
        {
            if (!_isOutlineEnabled && hitInfo.collider.TryGetComponent(out IClickableOutline clickableObjectOutline))
            {
                clickableObjectOutline.EnableOutline();
                _isOutlineEnabled = true;
            }
        }

        private void OnPlayerRaycastFinished(RaycastHit hitInfo)
        {
            if (_isOutlineEnabled && hitInfo.collider.TryGetComponent(out IClickableOutline clickableObjectOutline))
            {
                clickableObjectOutline.DisableOutline();
                _isOutlineEnabled = false;
            }
        }
    }
}
