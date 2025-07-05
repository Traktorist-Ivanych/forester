using UnityEngine;

namespace Interfaces
{
    public interface IPickableItem
    {
        public IPickableItem OriginalIPickableItem { get; }
        public Transform ItemTransform { get; }

        public void Pickup(Transform parent);

        public bool TryRelease();

        public bool TryThrow(Vector3 force);
    }
}