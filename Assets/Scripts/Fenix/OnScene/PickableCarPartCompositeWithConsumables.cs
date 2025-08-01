using UnityEngine;

namespace Fenix
{
    public class PickableCarPartCompositeWithConsumables : PickableCarPartComposite
    {
        [SerializeField] private CarPartConsumables[] _consumablesElements;

        public override void Install()
        {
            (_nearestFenixPart as FenixPartCompositeWithConsumables).InstallCompositeElementsConsumables(_consumablesElements);

            base.Install();
        }

        public void SetupConsumables(CarPartConsumables[] carPartConsumables)
        {
            for (int i = 0; i < _consumablesElements.Length; i++)
            {
                _consumablesElements[i].SetHealthPoints(carPartConsumables[i].HealthPoints);
            }
        }
    }
}