
using Interfaces;

namespace Tools
{
    public abstract class ToolLeftMouseButtonPickable : Tool, ILeftMouseClickable
    {
        public virtual void OnLeftMouseClicked()
        {
            throw new System.NotImplementedException();
        }
    }
}
