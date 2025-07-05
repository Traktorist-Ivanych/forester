using Interfaces;
using UnityEngine;
using Zenject;

namespace Player
{
    public class InputControllerClickable : MonoBehaviour
    {
        public void LeftClickRaycast(RaycastHit raycastHit)
        {
            if (raycastHit.collider.TryGetComponent(out ILeftMouseClickable leftMouseClickable))
            {
                leftMouseClickable.OnLeftMouseClicked();
            }
        }

        public void RightClickRaycast(RaycastHit raycastHit)
        {
            if (raycastHit.collider.TryGetComponent(out IRightMouseClickable leftMouseClickable))
            {
                leftMouseClickable.OnRightMouseClicked();
            }
        }
    }
}
