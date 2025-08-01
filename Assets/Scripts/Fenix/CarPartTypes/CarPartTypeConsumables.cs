using UnityEngine;

namespace Fenix
{
    public class CarPartTypeConsumables : CarPartType
    {
        [SerializeField] private ConsumablesPartType _consumablesPartType;

        public ConsumablesPartType ConsumablesPartType => _consumablesPartType;

        private void OnValidate()
        {
            carSystem = CarSystem.Consumables;
        }
    }
}
