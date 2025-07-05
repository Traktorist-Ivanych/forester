using System;
using Interfaces;
using Tools;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerToolManager : MonoBehaviour
    {
        [Inject] private PlayerInputManager _playerRaycast;

        [SerializeField] private Transform _originTransform;
        [SerializeField] private float _rayDistance;
        [SerializeField] private LayerMask _rayLayserMask;

        private bool _isTurnedOff = true;
        private bool _hasHit;
        private RaycastHit _raycastHit;

        public Action<RaycastHit> RaycastStartAction { get; set; }
        public Action<RaycastHit> RaycastFinishAction { get; set; }
        public Action<RaycastHit> LeftClickRaycastAction { get; set; }
        public Action<RaycastHit> RightClickRaycastAction { get; set; }

        private Tool _currentTool;

        private void Update()
        {
            if (_isTurnedOff) return;

            _hasHit = Physics.Raycast(
                origin: _originTransform.position,
                direction: _originTransform.forward,
                hitInfo: out RaycastHit hitInfo,
                maxDistance: _rayDistance,
                layerMask: _rayLayserMask);

            if (_hasHit)
            {

            }
        }

        public bool TryPickupTool(RaycastHit raycastHit)
        {
            if (raycastHit.collider.TryGetComponent(out ILeftMouseClickTool leftMouseClickTool))
            {
                if (leftMouseClickTool is Tool)
                {
                    _currentTool = leftMouseClickTool as Tool;

                    _isTurnedOff = false;

                    return true;
                }
            }

            return false;
        }
    }
}
