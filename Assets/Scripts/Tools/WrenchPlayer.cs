using UnityEngine;

namespace Tools
{
    public class WrenchPlayer : ToolLeftMouseButtonPickable
    {
        [SerializeField] private int _size;
        [SerializeField] private int _interactionSpeed;

        public int Size => _size;
        public int InteractionSpeed => _interactionSpeed;
    }
}
