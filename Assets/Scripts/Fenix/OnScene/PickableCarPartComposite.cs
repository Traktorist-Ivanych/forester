using UnityEngine;

namespace Fenix
{
    public class PickableCarPartComposite : PickableCarPart
    {
        [SerializeField] private CarPartCompositeElement[] _compositeElements;

        public bool CanElementBeInstalled(int elementIndex)
        {
            if (elementIndex >= _compositeElements.Length) return false;

            return !_compositeElements[elementIndex].IsInstalled;
        }

        public override void Install()
        {
            (_nearestFenixPart as FenixPartComposite).InstallCompositeElements(_compositeElements);

            base.Install();
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
            _compositeElements[elementIndex].Install(compositeElement);

            _compositeElements[elementIndex].DismantleAction += OnCompositeElementDismantle;
        }

        protected virtual void OnCompositeElementDismantle(CarPartCompositeElement compositeElement)
        {
            compositeElement.DismantleAction -= OnCompositeElementDismantle;
        }
    }
}