using System;
using Interfaces;
using UnityEngine;

namespace Fenix
{
    public class CarPartCompositeElement : MonoBehaviour, IRightMouseClickable
    {
        [SerializeField] private Transform _dismantleTransform;

        protected PickableCarPartCompositeElement _pickableCompositeElement;

        private bool _isInstalled;

        public Action<CarPartCompositeElement> DismantleAction { get; set; }
        public bool IsInstalled => _isInstalled;

        public virtual void Install(PickableCarPartCompositeElement pickableCompositeElement)
        {
            _pickableCompositeElement = pickableCompositeElement;
            _isInstalled = true;
            gameObject.SetActive(_isInstalled);
        }

        public void Setup(FenixPartCompositeElement fenixPartCompositeElement)
        {
            _isInstalled = fenixPartCompositeElement.IsInstalled;
            gameObject.SetActive(_isInstalled);
        }

        public virtual void OnRightMouseClicked()
        {
            _isInstalled = false;
            gameObject.SetActive(_isInstalled);
            _pickableCompositeElement.Dismantle(_dismantleTransform);
            DismantleAction?.Invoke(this);
        }
    }
}
