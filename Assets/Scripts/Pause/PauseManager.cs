using ProjectInput;
using UnityEngine;
using Zenject;

namespace Pause
{
    public class PauseManager : MonoBehaviour
    {
        [Inject] private readonly InputManager _inputManager;

        private bool _isPaused;

        private void Awake()
        {
            _inputManager.Activate();

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            _inputManager.EscapeButtonStartedAction += OnEscapeButtonPressed;
        }

        private void OnDisable()
        {
            _inputManager.EscapeButtonStartedAction -= OnEscapeButtonPressed;
        }

        private void OnEscapeButtonPressed()
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        private void Pause()
        {
            _inputManager.Deactivate();
            Cursor.lockState = CursorLockMode.None;
        }

        private void Play()
        {
            _inputManager.Activate();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
