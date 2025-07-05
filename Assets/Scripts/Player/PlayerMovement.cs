using ProjectInput;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Inject] private readonly InputManager _inputManager;

    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _movingSpeed;
    [SerializeField] private float _rotationSpeed;

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

    private void MovePlayer(Vector2 inputVector2)
    {
        Vector3 frontDiraction = _rb.transform.forward * inputVector2.y * _movingSpeed * Time.deltaTime;
        Vector3 sideDiraction = _rb.transform.right * inputVector2.x * _movingSpeed * Time.deltaTime;

        _rb.MovePosition(_rb.position + frontDiraction + sideDiraction);
    }

    private void RotateCamera(Vector2 inputVector2)
    {
        _rb.transform.rotation *= Quaternion.Euler(0, inputVector2.x * _rotationSpeed * Time.deltaTime, 0);
        //_rb.MoveRotation(_rb.rotation * Quaternion.Euler(0, inputVector2.x * _rotationSpeed * Time.deltaTime, 0));
    }

    private void Jump()
    {

    }

    private void EnableSprint()
    {

    }

    private void DisableSprint()
    {

    }
}
