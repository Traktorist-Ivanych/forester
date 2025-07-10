using System.Collections.Generic;
using Interfaces;
using Items;
using UnityEngine;

namespace Fenix
{
    public class PickableCarPart : PickableItem, IPickableItemCarPart
    {
        [SerializeField] protected List<FenixPart> _fenixParts;

        protected bool _canBeInstalled;
        protected FenixPart _nearestFenixPart;

        public bool CanBeInstalled => _canBeInstalled;

        private void Update()
        {
            if (_isInPlayerHands)
            {
                foreach (FenixPart fenixPart in _fenixParts)
                {
                    if (fenixPart.IsInstalled) continue;

                    if (Vector3.Distance(transform.position, fenixPart.transform.position) < 0.2f &&
                        fenixPart.CanBeInstalled())
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

        public virtual void Install()
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
}