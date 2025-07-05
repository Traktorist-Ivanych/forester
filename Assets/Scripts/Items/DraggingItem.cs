using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class DraggingItem : MonoBehaviour, IDraggingItem
{
    [SerializeField] private Transform _ancorDefaultParent;
    [SerializeField] private Transform _itemTransform;
    [SerializeField] private Rigidbody _ancorRigidbody;
    [SerializeField] private List<Collider> _zeroFrictionColliders;
    [SerializeField] private PhysicsMaterial _zeroFrictionMaterial;

    private bool _isDragging;

    void Start()
    {
        _ancorRigidbody.gameObject.SetActive(false);
    }

    public bool TryDrag(Transform targetParrent)
    {
        if (!_isDragging)
        {
            _isDragging = true;

            //_ancorRigidbody.transform.SetPositionAndRotation(_itemTransform.position, _itemTransform.rotation);
            _ancorRigidbody.transform.position = _itemTransform.position;
            _ancorRigidbody.linearVelocity = Vector3.zero;
            _ancorRigidbody.angularVelocity = Vector3.zero;
            _ancorRigidbody.transform.SetParent(targetParrent);
            _ancorRigidbody.gameObject.SetActive(true);

            foreach (Collider zeroFrictionCollider in _zeroFrictionColliders)
            {
                zeroFrictionCollider.material = _zeroFrictionMaterial;
            }

            return true;
        }

        return false;
    }

    public bool TryRelease()
    {
        if (_isDragging)
        {
            _isDragging = false;

            _ancorRigidbody.linearVelocity = Vector3.zero;
            _ancorRigidbody.angularVelocity = Vector3.zero;
            _ancorRigidbody.transform.SetParent(_ancorDefaultParent);
            _ancorRigidbody.gameObject.SetActive(false);

            foreach (Collider zeroFrictionCollider in _zeroFrictionColliders)
            {
                zeroFrictionCollider.material = null;
            }

            return true;
        }

        return false;
    }
}
