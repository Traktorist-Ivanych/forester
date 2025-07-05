using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectInput
{
    public class InputManager : MonoBehaviour
    {
        public Action<Vector2> MoveInputAction;
        public Action<Vector2> LookInputAction;
        public Action<Vector2> MouseScrollAction;

        public Action ShiftButtonStartedAction;
        public Action ShiftButtonCanceledAction;
        public Action SpaceButtonStartedAction;
        public Action LeftMouseButtonStartedAction;
        public Action RightMouseButtonStartedAction;
        public Action EscapeButtonStartedAction;

        private InputActions _inputActions;

        private bool _isActive;

        private void Awake()
        {
            _inputActions = new InputActions();
        }

        private void OnEnable()
        {
            _inputActions.Player.Enable();

            _inputActions.Player.Shift.started += OnShiftButtonStarted;
            _inputActions.Player.Shift.canceled += OnShiftButtonCanceled;
            _inputActions.Player.Space.started += OnSpaceButtonStarted;
            _inputActions.Player.LeftMouseClick.started += OnLeftMouseButtonStarted;
            _inputActions.Player.RightMouseClick.started += OnRightMouseButtonStarted;
            _inputActions.Player.Pause.started += OnEscapeButtonStarted;
        }

        private void OnDisable()
        {
            _inputActions.Player.Shift.started -= OnShiftButtonStarted;
            _inputActions.Player.Shift.canceled -= OnShiftButtonCanceled;
            _inputActions.Player.Space.started -= OnSpaceButtonStarted;
            _inputActions.Player.LeftMouseClick.started -= OnLeftMouseButtonStarted;
            _inputActions.Player.RightMouseClick.started -= OnRightMouseButtonStarted;
            _inputActions.Player.Pause.started -= OnEscapeButtonStarted;

            _inputActions.Player.Disable();
        }

        private void Update()
        {
            if (_isActive && _inputActions.Player.enabled)
            {
                MoveInputAction?.Invoke(_inputActions.Player.Move.ReadValue<Vector2>());
                LookInputAction?.Invoke(_inputActions.Player.Look.ReadValue<Vector2>());
                MouseScrollAction?.Invoke(_inputActions.Player.MouseScroll.ReadValue<Vector2>());
            }
        }

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }

        private void OnShiftButtonStarted(InputAction.CallbackContext context)
        {
            CheckClickability(ShiftButtonStartedAction);
        }

        private void OnShiftButtonCanceled(InputAction.CallbackContext context)
        {
            CheckClickability(ShiftButtonCanceledAction);
        }

        private void OnSpaceButtonStarted(InputAction.CallbackContext context)
        {
            CheckClickability(SpaceButtonStartedAction);
        }

        private void OnLeftMouseButtonStarted(InputAction.CallbackContext context)
        {
            CheckClickability(LeftMouseButtonStartedAction);
        }

        private void OnRightMouseButtonStarted(InputAction.CallbackContext context)
        {
            CheckClickability(RightMouseButtonStartedAction);
        }

        private void CheckClickability(Action actionToInvoke)
        {
            if (_isActive)
            {
                actionToInvoke?.Invoke();
            }
        }

        private void OnEscapeButtonStarted(InputAction.CallbackContext context)
        {
            EscapeButtonStartedAction?.Invoke();
        }
    }
}
