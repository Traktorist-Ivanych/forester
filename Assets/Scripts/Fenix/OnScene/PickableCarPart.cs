using System.Collections.Generic;
using Interfaces;
using Items;
using UnityEngine;

namespace Fenix
{
    public class PickableCarPart : PickableItem, IPickableItemCarPart
    {
        [SerializeField] private CarPartType _carPartType;

        protected List<FenixPart> _fenixParts;

        protected bool _canBeInstalled;
        protected FenixPart _nearestFenixPart;

        public CarPartType CarPartType => _carPartType;
        public bool CanBeInstalled => _canBeInstalled;

        private void Update()
        {
            if (_isInPlayerHands)
            {
                float minDistance = float.MaxValue;
                _canBeInstalled = false;
                _nearestFenixPart = null;

                foreach (FenixPart fenixPart in _fenixParts)
                {
                    if (fenixPart.IsInstalled) continue;

                    float distance = Vector3.Distance(transform.position, fenixPart.transform.position);

                    if (distance < 0.2f && fenixPart.CanBeInstalled())
                    {
                        _canBeInstalled = true;

                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            _nearestFenixPart = fenixPart;
                        }
                    }
                }
            }
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_carPartType == null)
            {
                TryGetComponent(out _carPartType);
            }
        }

        public void Initialize(List<FenixPart> fenixParts)
        {
            _fenixParts = fenixParts;
        }

        public virtual void Install()
        {
            _nearestFenixPart.Install(this);

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
            _nearestFenixPart = null;
        }
    }
}