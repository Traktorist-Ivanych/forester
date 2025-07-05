using System;
using Interfaces;
using UnityEngine;

namespace Buildings
{
    public class DoorClickableHandle : MonoBehaviour, ILeftMouseClickable
    {
        public Action ClickedAction;

        public void OnLeftMouseClicked()
        {
            ClickedAction?.Invoke();
        }
    }
}
