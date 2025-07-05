using Player;
using UI;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class ClickableObejctsTextManager : MonoBehaviour
    {
        [Inject] private readonly PlayerInputManager _playerRaycast;
        [Inject] private readonly HUDCanvas _hudCanvas;

        private bool _isTextEnabled;

        private void OnEnable()
        {
            _playerRaycast.RaycastStartAction += OnPlayerRaycastStarded;
            _playerRaycast.RaycastFinishAction += OnPlayerRaycastFinished;
        }

        private void OnDisable()
        {
            _playerRaycast.RaycastStartAction -= OnPlayerRaycastStarded;
            _playerRaycast.RaycastFinishAction -= OnPlayerRaycastFinished;
        }

        private void OnPlayerRaycastStarded(RaycastHit hitInfo)
        {
            if (!_isTextEnabled && hitInfo.collider.TryGetComponent(out IClickableObjectText clickableObjectText))
            {
                _hudCanvas.ShowClickableObjectsText(clickableObjectText.ClickableText);
                _isTextEnabled = true;
            }
        }

        private void OnPlayerRaycastFinished(RaycastHit hitInfo)
        {
            if (_isTextEnabled)
            {
                _hudCanvas.HideClickableObjectsText();
                _isTextEnabled = false;
            }
        }
    }
}
