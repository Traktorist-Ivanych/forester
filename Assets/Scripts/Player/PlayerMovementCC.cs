using System.Collections.Generic;
using ProjectInput;
using Settings;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovementCC : MonoBehaviour
    {
        [Inject] private readonly InputManager _inputManager;

        [Header("Settings")]
        [SerializeField] private List<PlayerMovementSettingsType> _settingsType;
        [SerializeField] private List<PlayerMovementSettings> _settings;

        [Header("MainComponents")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform playerCamera;

        private PlayerMovementSettings _currentSettings;
        private bool _isSprintPressed;
        private float _gravity;
        private float cameraRotationX;
        private float playerVelocityY;

        private void OnEnable()
        {
            _inputManager.MoveInputAction += MovePlayer;
            _inputManager.LookInputAction += RotateCamera;
            _inputManager.SpaceButtonStartedAction += Jump;
            _inputManager.ShiftButtonStartedAction += EnableSprint;
            _inputManager.ShiftButtonCanceledAction += DisableSprint;
        }

        private void OnDisable()
        {
            _inputManager.MoveInputAction -= MovePlayer;
            _inputManager.LookInputAction -= RotateCamera;
            _inputManager.SpaceButtonStartedAction -= Jump;
            _inputManager.ShiftButtonStartedAction -= EnableSprint;
            _inputManager.ShiftButtonCanceledAction -= DisableSprint;
        }

        private void Start()
        {
            _gravity = Physics.gravity.y;
            SetSettings(PlayerMovementSettingsType.Main);
        }

        private void Update()
        {
            playerVelocityY += _gravity * Time.deltaTime;

            _characterController.Move(new Vector3(0, playerVelocityY * Time.deltaTime, 0));

            if (_characterController.isGrounded && playerVelocityY < 0)
            {
                playerVelocityY = 0;
            }
        }

        public void SetSettings(PlayerMovementSettingsType playerMovementSettingsType)
        {
            _currentSettings = _settings[_settingsType.IndexOf(playerMovementSettingsType)];
        }

        private void MovePlayer(Vector2 inputVector2)
        {
            Vector3 moveDirection =
                _characterController.transform.forward * inputVector2.y +
                _characterController.transform.right * inputVector2.x;

            Vector3 moveValue = moveDirection * Time.deltaTime;

            if (_isSprintPressed)
            {
                moveValue *= _currentSettings.PlayerSprintSpeed;
            }
            else
            {
                moveValue *= _currentSettings.PlayerMoveSpeed;
            }

            _characterController.Move(moveValue);
        }

        private void RotateCamera(Vector2 inputVector2)
        {
            _characterController.transform.Rotate(inputVector2.x * _currentSettings.PlayerRotateSpeed * Vector3.up);

            cameraRotationX -= inputVector2.y * _currentSettings.PlayerRotateSpeed;
            cameraRotationX = Mathf.Clamp(cameraRotationX, -_currentSettings.CameraRotationXMax, _currentSettings.CameraRotationXMax);
            playerCamera.localRotation = Quaternion.Euler(cameraRotationX, 0, 0);
        }

        private void Jump()
        {
            _characterController.Move(new Vector3(0, -0.01f, 0));

            if (_characterController.isGrounded)
            {
                playerVelocityY += Mathf.Sqrt(_currentSettings.JumpHeight * -2f * _gravity);
            }

            _characterController.Move(new Vector3(0, 0.01f, 0));
        }

        private void EnableSprint()
        {
            _isSprintPressed = true;
        }

        private void DisableSprint()
        {
            _isSprintPressed = false;
        }
    }
}
