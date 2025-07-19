using Tools;
using UnityEngine;

namespace Fenix
{
    public class BoltWrench : Bolt
    {
        [SerializeField] private int _boltSize;

        protected override bool CanInteract(Tool tool, out int interactionSpeed)
        {
            if (!base.CanInteract(tool, out interactionSpeed)) return false;

            PlayerWrench wrenchPlayer = tool as PlayerWrench;

            if (_boltSize == wrenchPlayer.Size)
            {
                interactionSpeed = wrenchPlayer.InteractionSpeed;
                return true;
            }

            return false;
        }
    }
}
