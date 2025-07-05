using UnityEngine;

public class HingeJointAnchorSetter : MonoBehaviour
{
    [SerializeField] private HingeJoint _hingeJoint;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private Transform _connectedRigidbodyTransform;

    void Start()
    {
        _hingeJoint.autoConfigureConnectedAnchor = false;
    }

    void Update()
    {
        _hingeJoint.connectedAnchor = _connectedRigidbodyTransform.InverseTransformPoint(_targetTransform.position);
    }
}
