using Interfaces;
using UnityEngine;

public class DraggingItemIntermediary : MonoBehaviour, IDraggingItem
{
    [SerializeField] private MonoBehaviour _mainDraggingItemMonoBehaviour;

    private IDraggingItem _mainDraggingItem;

    private void Start()
    {
        _mainDraggingItem = _mainDraggingItemMonoBehaviour as IDraggingItem;
    }

    public bool TryDrag(Transform targetParrent)
    {
        return _mainDraggingItem.TryDrag(targetParrent);
    }

    public bool TryRelease()
    {
        return _mainDraggingItem.TryRelease();
    }
}
