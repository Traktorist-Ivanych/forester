using System;
using UnityEngine;

namespace Fenix
{
    public class CarPartConsumablesMaterial : CarPartConsumables
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Material _cleanMaterial;
        [SerializeField] private Material _brokenMaterial;
        [SerializeField] private float _brokenHealthPointsBorder;

        private bool _isBroken;

        public Action BrokenAction { get; set; }

        public override void SetHealthPoints(float healthPoints)
        {
            base.SetHealthPoints(healthPoints);

            _meshRenderer.material = _healthPoints >= _brokenHealthPointsBorder ? _cleanMaterial : _brokenMaterial;
        }

        public override void AddHealthPoints(float healthPoints)
        {
            base.AddHealthPoints(healthPoints);

            if (_isBroken && _healthPoints >= _brokenHealthPointsBorder)
            {
                _isBroken = false;
                _meshRenderer.material = _cleanMaterial;
            }
        }

        public override void RemoveHealthPoints(float healthPoints)
        {
            base.RemoveHealthPoints(healthPoints);

            if (!_isBroken && _healthPoints < _brokenHealthPointsBorder)
            {
                _isBroken = true;
                _meshRenderer.material = _brokenMaterial;

                BrokenAction?.Invoke();
            }
        }
    }
}
