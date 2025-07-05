using Interfaces;
using UnityEngine;

namespace Tools
{
    public abstract class Tool : MonoBehaviour, ILeftMouseClickable
    {
        public virtual void OnLeftMouseClicked()
        {

        }
    }
}
