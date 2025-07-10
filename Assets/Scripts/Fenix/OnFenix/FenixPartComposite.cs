using UnityEngine;

namespace Fenix
{
    public class FenixPartComposite : FenixPart
    {
        [SerializeField] private FenixPartCompositeElement[] _compositePartElements;

        public void InstallCompositeElements(CarPartCompositeElement[] _compositeElements)
        {
            for (int i = 0; i < _compositePartElements.Length; i++)
            {
                _compositePartElements[i].Install(_compositeElements[i]);
            }
        }

        protected override void Dismantle()
        {
            base.Dismantle();

            if (_isInstalled) return;

            (_pickableItemCarPart as PickableCarPartComposite).SetupElements(_compositePartElements);
        }
    }
}
