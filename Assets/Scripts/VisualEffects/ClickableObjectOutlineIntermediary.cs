using UnityEngine;

public class ClickableObjectOutlineIntermediary : MonoBehaviour, IClickableOutline
{
    [SerializeField] private MonoBehaviour _mainClickableOutlineMonoBehaviour;

    private IClickableOutline _mainClickableOutline;

    private void Start()
    {
        _mainClickableOutline = _mainClickableOutlineMonoBehaviour as IClickableOutline;
    }


    public void DisableOutline()
    {
        _mainClickableOutline.DisableOutline();
    }

    public void EnableOutline()
    {
        _mainClickableOutline.EnableOutline();
    }
}
