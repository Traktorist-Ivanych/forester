using UnityEngine;

namespace Fenix
{
    public class FenixPartWithConsumables : FenixPart
    {
        [SerializeField] private CarPartConsumables _carPartConsumables;

        public void InstallConsumables(CarPartConsumables carPartConsumables)
        {
            _carPartConsumables.SetHealthPoints(carPartConsumables.HealthPoints);
        }

        protected override void Dismantle()
        {
            base.Dismantle();

            if (_isInstalled) return;

            (_pickableItemCarPart as PickableCarPartWithConsumables).SetupConsumables(_carPartConsumables);
        }
    }
}
