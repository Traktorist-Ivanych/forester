using UnityEngine;

namespace Tools
{
    public class PlayerOilFilterWrench : ToolLeftMouseButtonPickable
    {
        [SerializeField] private int _interactionSpeed;

        public int InteractionSpeed => _interactionSpeed;
    }
}
