using UnityEngine;

namespace Interfaces
{
    public interface IDraggingItem
    {
        public bool TryDrag(Transform targetParrent);

        public bool TryRelease();
    }
}
