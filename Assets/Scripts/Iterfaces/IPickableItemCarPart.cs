using UnityEngine;

namespace Interfaces
{
    public interface IPickableItemCarPart
    {
        public bool CanBeInstalled { get; }

        public void Install();

        public void Dismantle(Transform carPartTransform);
    }
}
