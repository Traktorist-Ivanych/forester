using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace Items
{
    public class PickableItem : MonoBehaviour, IPickableItem
    {
        [SerializeField] private Transform _mainParent;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private List<Collider> _colliders;

        private List<Collider> _collisions = new();
        protected bool _isInPlayerHands;

        public IPickableItem OriginalIPickableItem => this;
        public Transform ItemTransform => transform;

        private void OnValidate()
        {
            if (_rigidbody == null && TryGetComponent(out Rigidbody rigidbody))
            {
                _rigidbody = rigidbody;
            }

            if (_colliders.Count <= 0)
            {
                _colliders = GetComponentsInChildren<Collider>().ToList();

                for (int i = _colliders.Count - 1; i >= 0; i--)
                {
                    if (_colliders.Count == 0) break;

                    if (_colliders[i].isTrigger)
                    {
                        _colliders.Remove(_colliders[i]);
                    }
                    if (_colliders[i].gameObject.layer == 10)
                    {
                        _colliders.Remove(_colliders[i]);
                    }
                }
            }

            if (_mainParent == null)
            {
                _mainParent = transform.parent;
            }
        }

        // private void Start()
        // {
        //     for (int i = 0; i < _colliders.Count; i++)
        //     {
        //         for (int j = i + 1; j < _colliders.Count; j++)
        //         {
        //             Physics.IgnoreCollision(_colliders[i], _colliders[j]);
        //         }
        //     }
        // }

        private void OnTriggerEnter(Collider other)
        {
            if (_collisions.Contains(other)) return;
            _collisions.Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (_collisions.Contains(other))
            {
                _collisions.Remove(other);
            }
        }

        public void Pickup(Transform parent)
        {
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;

            _collisions.Clear();

            foreach (Collider collider in _colliders)
            {
                collider.isTrigger = true;
            }

            transform.parent = parent;

            _isInPlayerHands = true;
        }

        public bool TryRelease()
        {
            if (_collisions.Count != 0)
            {
                for (int i = _collisions.Count - 1; i >= 0; i--)
                {
                    if (!_collisions[i].gameObject.activeInHierarchy)
                    {
                        _collisions.Remove(_collisions[i]);
                    }
                }
            }

            if (_collisions.Count == 0)
            {
                Release();

                return true;
            }

            return false;
        }

        protected void Release()
        {
            transform.parent = _mainParent;

            foreach (Collider collider in _colliders)
            {
                collider.isTrigger = false;
            }
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;

            _isInPlayerHands = false;
        }

        public bool TryThrow(Vector3 force)
        {
            if (TryRelease())
            {
                _rigidbody.AddForce(force, ForceMode.Impulse);

                return true;
            }

            return false;
        }
    }
}