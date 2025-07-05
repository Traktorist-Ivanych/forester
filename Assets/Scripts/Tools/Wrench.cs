using UnityEngine;

namespace Tools
{
    public class Wrench : Tool
    {
        [SerializeField] private int _size;
        [SerializeField] private int _interactionSpeed;

        public int Size => _size;
        public int InteractionSpeed => _interactionSpeed;

        public override void OnLeftMouseClicked()
        {


        }
    }
}
