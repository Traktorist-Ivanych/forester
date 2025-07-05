using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "PlayerMovement", menuName = "Settings/PlayerMovement")]
    public class PlayerMovementSettings : ScriptableObject
    {
        [Header("PlayerMoving")]
        [field: SerializeField] public float PlayerMoveSpeed { get; private set; }
        [field: SerializeField] public float PlayerSprintSpeed { get; private set; }

        [Header("CameraMoving")]
        [field: SerializeField] public float PlayerRotateSpeed { get; private set; }
        [field: SerializeField] public float CameraRotationXMax { get; private set; }

        [Header("Jump")]
        [field: SerializeField] public float JumpHeight { get; private set; }
    }

    public enum PlayerMovementSettingsType
    {
        Main,
        Drag
    }
}
