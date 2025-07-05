using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class DoorOnHingeJoint : MonoBehaviour
    {
        [SerializeField] private List<DoorClickableHandle> _doorClickableHandles;
        [SerializeField] private HingeJoint _hingeJoint;
        [SerializeField] private float _openTargetPosition;
        [SerializeField] private float _closeTargetPosition;

        private bool _isOpen;

        public bool IsOpen => _isOpen;

        private void OnEnable()
        {
            foreach (DoorClickableHandle doorClickableHandle in _doorClickableHandles)
            {
                doorClickableHandle.ClickedAction += OnHandleClicked;
            }
        }

        private void OnDisable()
        {
            foreach (DoorClickableHandle doorClickableHandle in _doorClickableHandles)
            {
                doorClickableHandle.ClickedAction -= OnHandleClicked;
            }
        }

        public void OnHandleClicked()
        {
            JointSpring jointSpring = _hingeJoint.spring;
            jointSpring.targetPosition = _isOpen ? _closeTargetPosition : _openTargetPosition;

            _hingeJoint.spring = jointSpring;

            _isOpen = !_isOpen;
        }
    }
}
