using System.Collections.Generic;
using Interfaces;
using Items;
using UnityEngine;

namespace Fenix
{
    public class PickableCarPartCompositeElement : PickableItem, IPickableItemCarPart
    {
        [SerializeField] private CarPartType _carPartType;

        private List<CarPartCompositeElement> _compositeParts;

        private bool _canBeInstalled;
        private CarPartCompositeElement _nearestCompositePart;

        public CarPartType CarPartType => _carPartType;
        public bool CanBeInstalled => _canBeInstalled;

        private void Update()
        {
            if (_isInPlayerHands)
            {
                float minDistance = float.MaxValue;
                _canBeInstalled = false;
                _nearestCompositePart = null;

                foreach (CarPartCompositeElement compositePart in _compositeParts)
                {
                    if (compositePart.IsInstalled) continue;

                    float distance = Vector3.Distance(transform.position, compositePart.transform.position);

                    if (distance < 0.2f)
                    {
                        _canBeInstalled = true;
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            _nearestCompositePart = compositePart;
                        }
                    }
                }
            }
        }

        public void Initialize(List<CarPartCompositeElement> compositeParts)
        {
            _compositeParts = compositeParts;
        }

        public void Install()
        {
            _nearestCompositePart.Install(this);

            Release();

            gameObject.SetActive(false);
        }

        public void Dismantle(Transform fenixPartTransform)
        {
            gameObject.SetActive(true);
            gameObject.transform.SetPositionAndRotation(fenixPartTransform.position, fenixPartTransform.rotation);
        }

        protected override void Release()
        {
            base.Release();

            _canBeInstalled = false;
            _nearestCompositePart = null;
        }
    }
}