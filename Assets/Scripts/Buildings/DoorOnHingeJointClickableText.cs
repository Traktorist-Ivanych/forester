using UnityEngine;
using Zenject;

namespace Buildings
{
    [RequireComponent(typeof(DoorOnHingeJoint))]
    public class DoorOnHingeJointClickableText : MonoBehaviour, IClickableObjectText
    {
        [SerializeField] private string _openDoorText;
        [SerializeField] private string _closeDoorText;

        [SerializeField, HideInInspector] private DoorOnHingeJoint _doorOnHingeJoint;

        public string ClickableText
        {
            get
            {
                if (_doorOnHingeJoint == null) return string.Empty;

                return _doorOnHingeJoint.IsOpen ? _closeDoorText : _openDoorText;
            }
        }

        private void OnValidate()
        {
            if (_doorOnHingeJoint == null) _doorOnHingeJoint = GetComponent<DoorOnHingeJoint>();
        }
    }
}
