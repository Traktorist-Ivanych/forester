using System.Collections.Generic;
using Interfaces;
using Items;
using UnityEngine;

namespace Fenix
{
    public class PickableCarPartCompositeElement : PickableItem, IPickableItemCarPart
    {
        [SerializeField] private List<PickableCarPartComposite> _compositeParts;
        [SerializeField] private int _elementIndex;

        private bool _canBeInstalled;
        private PickableCarPartComposite _nearestCompositePart;

        public bool CanBeInstalled => _canBeInstalled;


        private void Update()
        {
            if (_isInPlayerHands)
            {
                foreach (PickableCarPartComposite compositePart in _compositeParts)
                {
                    if (!compositePart.CanElementBeInstalled(_elementIndex)) continue;

                    if (Vector3.Distance(transform.position, compositePart.transform.position) < 0.2f)
                    {
                        _canBeInstalled = true;
                        _nearestCompositePart = compositePart;

                        break;
                    }
                    else
                    {
                        _canBeInstalled = false;
                        _nearestCompositePart = null;
                    }
                }
            }
        }

        public void Install()
        {
            Release();

            _nearestCompositePart.InstallCompositeElement(this, _elementIndex);

            gameObject.SetActive(false);
        }

        public void Dismantle(Transform fenixPartTransform)
        {
            gameObject.SetActive(true);
            gameObject.transform.SetPositionAndRotation(fenixPartTransform.position, fenixPartTransform.rotation);
        }
    }
}