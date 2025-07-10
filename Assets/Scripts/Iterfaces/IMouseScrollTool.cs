using Tools;
using UnityEngine;

namespace Interfaces
{
    public interface IMouseScrollTool
    {
        public bool TryUseMouseScrollTool(Vector2 input, Tool tool);
    }
}
