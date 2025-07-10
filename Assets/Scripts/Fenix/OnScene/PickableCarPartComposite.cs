using UnityEngine;

namespace Fenix
{
    public class PickableCarPartComposite : PickableCarPart
    {
        [SerializeField] private PickableCarPartCompositeElement[] _pickableCompositeElements;
        [SerializeField] private CarPartCompositeElement[] _compositeElements;

        public bool CanElementBeInstalled(int elementIndex)
        {
            if (elementIndex >= _pickableCompositeElements.Length) return false;

            return _pickableCompositeElements[elementIndex] == null;
        }

        public override void Install()
        {
            base.Install();

            (_nearestFenixPart as FenixPartComposite).InstallCompositeElements(_compositeElements);
        }

        public void SetupElements(FenixPartCompositeElement[] compositePartElements)
        {
            for (int i = 0; i < _compositeElements.Length; i++)
            {
                _compositeElements[i].Setup(compositePartElements[i]);
            }
        }

        public virtual void InstallCompositeElement(PickableCarPartCompositeElement compositeElement, int elementIndex)
        {
            _pickableCompositeElements[elementIndex] = compositeElement;
            _compositeElements[elementIndex].Install(compositeElement);

            _compositeElements[elementIndex].DismantleAction += OnCompositeElementDismantle;
        }

        protected virtual void OnCompositeElementDismantle(CarPartCompositeElement compositeElement)
        {
            compositeElement.DismantleAction -= OnCompositeElementDismantle;
            _pickableCompositeElements[System.Array.IndexOf(_compositeElements, compositeElement)] = null;
        }
    }
}