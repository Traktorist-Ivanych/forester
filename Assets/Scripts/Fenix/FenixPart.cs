using Interfaces;
using UnityEngine;

public class FenixPart : MonoBehaviour, IRightMouseClickable
{
    [SerializeField] private GameObject _fenixPartObject;

    private IPickableItemCarPart _pickableItemCarPart;
    private bool _isInstalled;

    public bool IsInstalled => _isInstalled;

    public void Install(IPickableItemCarPart pickableItemCarPart)
    {
        _pickableItemCarPart = pickableItemCarPart;
        _fenixPartObject.SetActive(true);

        _isInstalled = true;
    }

    public void OnRightMouseClicked()
    {
        if (!_isInstalled) return;

        _isInstalled = false;

        _fenixPartObject.SetActive(false);

        _pickableItemCarPart.Dismantle(transform);
    }
}
