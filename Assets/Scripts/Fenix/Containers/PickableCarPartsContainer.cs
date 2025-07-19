using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using Zenject;

namespace Fenix
{
    public class PickableCarPartsContainer : MonoBehaviour
    {
        [Inject] private readonly FenixPartsContainer _fenixPartsContainer;
        [Inject] private readonly PickableCarPartCompositeElementsContainer _compositeElementsContainer;

        [SerializeField] private List<PickableCarPart> _allPickableCarParts;

        private Dictionary<EnginePartType, List<PickableCarPart>> _enginePartsContainer = new();
        private Dictionary<ConsumablesPartType, List<PickableCarPart>> _consumablesPartsContainer = new();

        private void Awake()
        {
            FillContainers();

            _compositeElementsContainer.InitializeCarPartCompositeElements();
        }

        public void InitializePickableCarParts()
        {
            foreach (PickableCarPart pickableCarPart in _allPickableCarParts)
            {
                pickableCarPart.Initialize(_fenixPartsContainer.GetFenixParts(pickableCarPart.CarPartType));
            }
        }

        public List<PickableCarPart> GetPickableCarParts(CarPartType carPartType)
        {
            switch (carPartType.CarSystem)
            {
                case CarSystem.Engine:
                    EnginePartType enginePartType = (carPartType as CarPartTypeEngine).EnginePartType;
                    return _enginePartsContainer[enginePartType];

                case CarSystem.Consumables:
                    ConsumablesPartType consumablesPartType = (carPartType as CarPartTypeConsumables).ConsumablesPartType;
                    return _consumablesPartsContainer[consumablesPartType];

                default:
                    Debug.LogError($"PickableCarPartsContainer not contains CarSystem {carPartType.CarSystem}");
                    return null;
            }
        }

        private void FillContainers()
        {
            foreach (PickableCarPart pickableCarPart in _allPickableCarParts)
            {
                switch (pickableCarPart.CarPartType.CarSystem)
                {
                    case CarSystem.Engine:
                        EnginePartType enginePartType =
                            (pickableCarPart.CarPartType as CarPartTypeEngine).EnginePartType;
                        if (_enginePartsContainer.ContainsKey(enginePartType))
                        {
                            _enginePartsContainer[enginePartType].Add(pickableCarPart);
                        }
                        else
                        {
                            _enginePartsContainer.Add(enginePartType, new List<PickableCarPart>() { pickableCarPart });
                        }
                        break;

                    case CarSystem.Consumables:
                        ConsumablesPartType consumablesPartType =
                            (pickableCarPart.CarPartType as CarPartTypeConsumables).ConsumablesPartType;
                        if (_consumablesPartsContainer.ContainsKey(consumablesPartType))
                        {
                            _consumablesPartsContainer[consumablesPartType].Add(pickableCarPart);
                        }
                        else
                        {
                            _consumablesPartsContainer.Add(consumablesPartType, new List<PickableCarPart>() { pickableCarPart });
                        }
                        break;
                }
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Fill All Pickable Car Parts List")]
        private void FillAllPickableCarParts()
        {
            StageHandle stage = StageUtility.GetCurrentStageHandle();
            _allPickableCarParts = stage.FindComponentsOfType<PickableCarPart>().ToList();
        }
#endif
    }
}
