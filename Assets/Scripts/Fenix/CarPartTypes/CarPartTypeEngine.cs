using UnityEngine;

namespace Fenix
{
    public class CarPartTypeEngine : CarPartType
    {
        [SerializeField] private EnginePartType _enginePartType;

        public EnginePartType EnginePartType => _enginePartType;

        private void OnValidate()
        {
            carSystem = CarSystem.Engine;
        }
    }
}
