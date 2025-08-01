using UnityEngine;

namespace Fenix
{
    public class CarPartCompositeElementConsumables : CarPartCompositeElement
    {
        [SerializeField] private CarPartConsumables _carPartConsumables;

        public CarPartConsumables CarPartConsumables => _carPartConsumables;

        public override void Install(PickableCarPartCompositeElement pickableCompositeElement)
        {
            CarPartConsumables consumables = (pickableCompositeElement as PickableCarPartCompositeElementConsumables)
                .CarPartConsumables;

            _carPartConsumables.SetHealthPoints(consumables.HealthPoints);

            base.Install(pickableCompositeElement);
        }

        public override void OnRightMouseClicked()
        {
            base.OnRightMouseClicked();

            (_pickableCompositeElement as PickableCarPartCompositeElementConsumables).CarPartConsumables
                .SetHealthPoints(_carPartConsumables.HealthPoints);
        }
    }
}
