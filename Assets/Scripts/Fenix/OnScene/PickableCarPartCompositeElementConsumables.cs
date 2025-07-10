using UnityEngine;

namespace Fenix
{
    public class PickableCarPartCompositeElementConsumables : PickableCarPartCompositeElement
    {
        [SerializeField] private CarPartConsumables _carPartConsumables;

        public CarPartConsumables CarPartConsumables => _carPartConsumables;
    }
}
