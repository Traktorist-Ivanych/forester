using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class PickableItemCarPart : PickableItem, IPickableItemCarPart
{
    [SerializeField] private List<FenixPart> _fenixParts;

    private bool _canBeInstalled;

    public bool CanBeInstalled => _canBeInstalled;

    private FenixPart _nearestFenixPart;

    private void Update()
    {
        if (_isInPlayerHands)
        {
            foreach (FenixPart fenixPart in _fenixParts)
            {
                if (fenixPart.IsInstalled) continue;

                if (Vector3.Distance(transform.position, fenixPart.transform.position) < 0.2f)
                {
                    _canBeInstalled = true;
                    _nearestFenixPart = fenixPart;

                    break;
                }
                else
                {
                    _canBeInstalled = false;
                    _nearestFenixPart = null;
                }
            }
        }
    }

    public void Install()
    {
        Release();

        _nearestFenixPart.Install(this);

        gameObject.SetActive(false);
    }

    public void Dismantle(Transform fenixPartTransform)
    {
        gameObject.SetActive(true);
        gameObject.transform.SetPositionAndRotation(fenixPartTransform.position, fenixPartTransform.rotation);
    }
}
