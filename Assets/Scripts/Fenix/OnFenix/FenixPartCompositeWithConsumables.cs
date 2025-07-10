using UnityEngine;

namespace Fenix
{
    public class FenixPartCompositeWithConsumables : FenixPartComposite
    {
        [SerializeField] private CarPartConsumables[] _compositePartElementsConsumables;

        public void InstallCompositeElementsConsumables(CarPartConsumables[] compositeElementsConsumables)
        {
            for (int i = 0; i < _compositePartElementsConsumables.Length; i++)
            {
                _compositePartElementsConsumables[i].SetHealthPoints(compositeElementsConsumables[i].HealthPoints);
            }
        }

        protected override void Dismantle()
        {
            base.Dismantle();

            if (_isInstalled) return;

            (_pickableItemCarPart as PickableCarPartCompositeWithConsumables).SetupConsumables(_compositePartElementsConsumables);
        }
    }
}
