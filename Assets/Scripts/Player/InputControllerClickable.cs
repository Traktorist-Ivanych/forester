using Interfaces;
using UnityEngine;
using Zenject;

namespace Player
{
    public class InputControllerClickable : MonoBehaviour
    {
        public bool TryUseLeftClickRaycast(RaycastHit raycastHit)
        {
            if (raycastHit.collider.TryGetComponent(out ILeftMouseClickable leftMouseClickable))
            {
                leftMouseClickable.OnLeftMouseClicked();

                return true;
            }

            return false;
        }

        public bool TryUseRightClickRaycast(RaycastHit raycastHit)
        {
            if (raycastHit.collider.TryGetComponent(out IRightMouseClickable leftMouseClickable))
            {
                leftMouseClickable.OnRightMouseClicked();

                return true;
            }

            return false;
        }
    }
}
