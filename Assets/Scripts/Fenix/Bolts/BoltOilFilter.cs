using Tools;

namespace Fenix
{
    public class BoltOilFilter : Bolt
    {
        protected override bool CanInteract(Tool tool, out int interactionSpeed)
        {
            if (!base.CanInteract(tool, out interactionSpeed)) return false;

            PlayerOilFilterWrench wrenchPlayer = tool as PlayerOilFilterWrench;

            interactionSpeed = wrenchPlayer.InteractionSpeed;

            return true;
        }
    }
}
