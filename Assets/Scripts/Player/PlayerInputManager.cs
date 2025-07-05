using System;
using ProjectInput;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerInputManager : MonoBehaviour
    {
        [Inject] private readonly InputManager _inputManager;

        [Header("Settings")]
        [SerializeField] private Transform _originTransform;
        [SerializeField] private float _rayDistance;
        [SerializeField] private LayerMask _rayLayserMask;

        [Header("Controllers")]
        [SerializeField] private InputControllerClickable _inputControllerClickable;
        [SerializeField] private InputControllerDragging _inputControllerDragging;
        [SerializeField] private InputControllerPickable _inputControllerPickable;

        private PlayerInputType _currentInputType;

        private bool _hasHit;
        private RaycastHit _raycastHit;

        public Action<RaycastHit> RaycastStartAction { get; set; }
        public Action<RaycastHit> RaycastFinishAction { get; set; }

        private void Awake()
        {
            _currentInputType = PlayerInputType.Clickable;
        }

        private void OnEnable()
        {
            _inputManager.LeftMouseButtonStartedAction += OnMouseLeftButtonClicked;
            _inputManager.RightMouseButtonStartedAction += OnMouseRightButtonClicked;
            _inputManager.MouseScrollAction += OnMouseScrolled;
        }

        private void OnDisable()
        {
            _inputManager.LeftMouseButtonStartedAction -= OnMouseLeftButtonClicked;
            _inputManager.RightMouseButtonStartedAction -= OnMouseRightButtonClicked;
            _inputManager.MouseScrollAction -= OnMouseScrolled;
        }

        private void Update()
        {
            if (_currentInputType == PlayerInputType.Pickable ||
                _currentInputType == PlayerInputType.Dragging)
                return;

            _hasHit = Physics.Raycast(
                origin: _originTransform.position,
                direction: _originTransform.forward,
                hitInfo: out RaycastHit hitInfo,
                maxDistance: _rayDistance,
                layerMask: _rayLayserMask);

            if (_hasHit)
            {
                if (_raycastHit.collider == null)
                {
                    _raycastHit = hitInfo;
                    RaycastStartAction?.Invoke(_raycastHit);
                }
                else if (!_raycastHit.collider.Equals(hitInfo.collider))
                {
                    RaycastFinishAction?.Invoke(_raycastHit);
                    _raycastHit = hitInfo;
                    RaycastStartAction?.Invoke(_raycastHit);
                }
                else
                {
                    _raycastHit = hitInfo;
                }
            }
            else if (_raycastHit.collider != null)
            {
                InvokeRaycastFinishAction();
            }
        }

        private void OnMouseLeftButtonClicked()
        {
            switch (_currentInputType)
            {
                case PlayerInputType.Clickable:
                    if (_hasHit)
                    {
                        if (_inputControllerPickable.TryPickupObject(_raycastHit))
                        {
                            InvokeRaycastFinishAction();
                            _currentInputType = PlayerInputType.Pickable;
                        }
                        else if (_inputControllerDragging.TryDragObject(_raycastHit))
                        {
                            InvokeRaycastFinishAction();
                            _currentInputType = PlayerInputType.Dragging;
                        }
                        else
                        {
                            _inputControllerClickable.LeftClickRaycast(_raycastHit);
                        }
                    }
                    break;

                case PlayerInputType.Dragging:
                    if (_inputControllerDragging.TryReleaseObject())
                    {
                        _currentInputType = PlayerInputType.Clickable;
                    }
                    break;

                case PlayerInputType.Pickable:
                    if (_inputControllerPickable.TryReleaseObject())
                    {
                        _currentInputType = PlayerInputType.Clickable;
                    }
                    break;

                default:
                    return;
            }
        }

        private void OnMouseRightButtonClicked()
        {
            switch (_currentInputType)
            {
                case PlayerInputType.Clickable:
                    if (_hasHit)
                    {
                        _inputControllerClickable.RightClickRaycast(_raycastHit);
                    }
                    break;

                case PlayerInputType.Pickable:
                    if (_inputControllerPickable.TryThrowObject())
                    {
                        _currentInputType = PlayerInputType.Clickable;
                    }
                    break;

                default:
                    return;
            }
        }

        private void OnMouseScrolled(Vector2 input)
        {
            switch (_currentInputType)
            {
                case PlayerInputType.Pickable:
                    _inputControllerPickable.RotatePickableItem(input);
                    break;

                default:
                    return;
            }
        }

        private void InvokeRaycastFinishAction()
        {
            RaycastFinishAction?.Invoke(_raycastHit);
            _raycastHit = new();
        }
    }

    public enum PlayerInputType
    {
        None,
        Clickable,
        Pickable,
        Dragging,
        Tool
    }
}
