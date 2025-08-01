using UnityEngine;

namespace Fenix
{
    public class PickableCarPartWithConsumables : PickableCarPart
    {
        [SerializeField] private CarPartConsumables _consumables;

        public override void Install()
        {
            (_nearestFenixPart as FenixPartWithConsumables).InstallConsumables(_consumables);

            base.Install();
        }

        public void SetupConsumables(CarPartConsumables carPartConsumables)
        {
            _consumables.SetHealthPoints(carPartConsumables.HealthPoints);
        }
    }
}
