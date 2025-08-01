using System;
using Interfaces;
using UnityEngine;
using Zenject;

namespace Fenix
{
    public class CarPartCompositeElement : MonoBehaviour, IRightMouseClickable
    {
        [Inject] private readonly PickableCarPartCompositeElementsContainer _compositeElementsContainer;

        [SerializeField] private Transform _dismantleTransform;
        [SerializeField] private CarPartType _carPartType;

        protected PickableCarPartCompositeElement _pickableCompositeElement;

        private bool _isInstalled;

        public Action<CarPartCompositeElement> DismantleAction { get; set; }
        public bool IsInstalled => _isInstalled;
        public CarPartType CarPartType => _carPartType;

        public virtual void Install(PickableCarPartCompositeElement pickableCompositeElement)
        {
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
            _pickableCompositeElement = _compositeElementsContainer.GetPickableCarPartElement(_carPartType);
            _pickableCompositeElement.Dismantle(_dismantleTransform);
            DismantleAction?.Invoke(this);
        }
    }
}
