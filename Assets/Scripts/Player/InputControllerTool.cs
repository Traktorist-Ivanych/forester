using Interfaces;
using Tools;
using UnityEngine;

public class InputControllerTool : MonoBehaviour
{
    [SerializeField] private Transform _handsTransform;

    private Tool _currentTool;

    public bool TryTakeLeftMousePickableTool(RaycastHit raycastHit)
    {
        if (_currentTool != null) return false;

        if (raycastHit.collider.TryGetComponent(out ToolLeftMouseButtonPickable tool))
        {
            _currentTool = tool.GetTool();
            _currentTool.transform.SetParent(_handsTransform, false);

            return true;
        }

        return false;
    }

    public bool TryReleaseTool(RaycastHit raycastHit)
    {
        if (_currentTool == null) return false;

        if (raycastHit.collider.TryGetComponent(out ToolLeftMouseButtonPickable tool) &&
            tool.GetTool().Equals(_currentTool) && tool is ILeftMouseClickable)
        {
            (tool as ILeftMouseClickable).OnLeftMouseClicked();
            _currentTool = null;

            return true;
        }

        return false;
    }

    public bool TryUseToolByLeftMouseClick(RaycastHit raycastHit)
    {
        return false;
    }

    public bool TryUseToolByMouseScroll(RaycastHit raycastHit, Vector2 input)
    {
        if (_currentTool == null) return false;

        if (raycastHit.collider.TryGetComponent(out IMouseScrollTool mouseScrollTool))
        {
            if (mouseScrollTool.TryUseMouseScrollTool(input, _currentTool))
            {
                return true;
            }
        }

        return false;
    }
}
