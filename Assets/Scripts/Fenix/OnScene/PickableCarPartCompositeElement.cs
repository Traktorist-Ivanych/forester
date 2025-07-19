using System.Collections.Generic;
using Interfaces;
using Items;
using UnityEngine;

namespace Fenix
{
    public class PickableCarPartCompositeElement : PickableItem, IPickableItemCarPart
    {
        [SerializeField] private CarPartType _carPartType;
        [SerializeField] private CarPartType _parentCarPartType;

        private List<PickableCarPartComposite> _compositeParts;
        [SerializeField] private int _elementIndex;

        private bool _canBeInstalled;
        private PickableCarPartComposite _nearestCompositePart;

        public CarPartType CarPartType => _carPartType;
        public CarPartType ParentCarPartType => _parentCarPartType;
        public bool CanBeInstalled => _canBeInstalled;

        private void Update()
        {
            if (_isInPlayerHands)
            {
                float minDistance = float.MaxValue;
                _canBeInstalled = false;
                _nearestCompositePart = null;

                foreach (PickableCarPartComposite compositePart in _compositeParts)
                {
                    if (!compositePart.CanElementBeInstalled(_elementIndex)) continue;

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

        public void Initialize(List<PickableCarPart> compositeParts)
        {
            _compositeParts = new List<PickableCarPartComposite>();

            foreach (PickableCarPart pickableCarPart in compositeParts)
            {
                _compositeParts.Add(pickableCarPart as PickableCarPartComposite);
            }
        }

        public void Install()
        {
            _nearestCompositePart.InstallCompositeElement(this, _elementIndex);

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