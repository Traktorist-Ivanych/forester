using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Fenix
{
    public class PickableCarPartCompositeElementsContainer : MonoBehaviour
    {
        [SerializeField] private List<PickableCarPartCompositeElement> _allPickableCarPartElements;
        [SerializeField] private List<CarPartCompositeElement> _allCompositeElements;

        [SerializeField] private List<PickableCarPartCompositeElement> _consumablesOriginals;

        private Dictionary<EnginePartType, List<PickableCarPartCompositeElement>> _enginePickablePartsContainer = new();
        private Dictionary<ConsumablesPartType, List<PickableCarPartCompositeElement>> _consumablesPickablePartsContainer = new();

        private Dictionary<EnginePartType, List<CarPartCompositeElement>> _enginePartsContainer = new();
        private Dictionary<ConsumablesPartType, List<CarPartCompositeElement>> _consumablesPartsContainer = new();

        private Dictionary<ConsumablesPartType, PickableCarPartCompositeElement> _consumablesOriginalsContainer = new();

        private void Awake()
        {
            FillContainers();
            FillPickableContainers();
            FillConsumablesOriginalsContainer();

            InitializeCarPartCompositeElements();
        }

        public PickableCarPartCompositeElement GetPickableCarPartElement(CarPartType carPartType)
        {
            switch (carPartType.CarSystem)
            {
                case CarSystem.Engine:
                    EnginePartType enginePartType = (carPartType as CarPartTypeEngine).EnginePartType;
                    foreach (PickableCarPartCompositeElement pickableCarPart in _enginePickablePartsContainer[enginePartType])
                    {
                        if (!pickableCarPart.gameObject.activeInHierarchy)
                        {
                            return pickableCarPart;
                        }
                    }

                    Debug.LogError("Not enough unactive PickableCarPartCompositeElement!");
                    return null;

                case CarSystem.Consumables:
                    ConsumablesPartType consumablesPartType = (carPartType as CarPartTypeConsumables).ConsumablesPartType;
                    foreach (PickableCarPartCompositeElement pickableCarPart in _consumablesPickablePartsContainer[consumablesPartType])
                    {
                        if (!pickableCarPart.gameObject.activeInHierarchy)
                        {
                            return pickableCarPart;
                        }
                    }

                    PickableCarPartCompositeElement consumablesCopy =
                        Instantiate(_consumablesOriginalsContainer[consumablesPartType]);
                    _consumablesPickablePartsContainer[consumablesPartType].Add(consumablesCopy);

                    return consumablesCopy;

                default:
                    Debug.LogError($"PickableCarPartsContainer not contains CarSystem {carPartType.CarSystem}");
                    return null;
            }
        }

        private void FillContainers()
        {
            foreach (PickableCarPartCompositeElement carPartElement in _allPickableCarPartElements)
            {
                switch (carPartElement.CarPartType.CarSystem)
                {
                    case CarSystem.Engine:
                        EnginePartType enginePartType =
                            (carPartElement.CarPartType as CarPartTypeEngine).EnginePartType;
                        if (_enginePickablePartsContainer.ContainsKey(enginePartType))
                        {
                            _enginePickablePartsContainer[enginePartType].Add(carPartElement);
                        }
                        else
                        {
                            _enginePickablePartsContainer.Add(enginePartType, new List<PickableCarPartCompositeElement>()
                                { carPartElement });
                        }
                        break;

                    case CarSystem.Consumables:
                        ConsumablesPartType consumablesPartType =
                            (carPartElement.CarPartType as CarPartTypeConsumables).ConsumablesPartType;
                        if (_consumablesPickablePartsContainer.ContainsKey(consumablesPartType))
                        {
                            _consumablesPickablePartsContainer[consumablesPartType].Add(carPartElement);
                        }
                        else
                        {
                            _consumablesPickablePartsContainer.Add(consumablesPartType, new List<PickableCarPartCompositeElement>()
                                { carPartElement });
                        }
                        break;
                }
            }
        }

        private void FillPickableContainers()
        {
            foreach (CarPartCompositeElement carPartElement in _allCompositeElements)
            {
                switch (carPartElement.CarPartType.CarSystem)
                {
                    case CarSystem.Engine:
                        EnginePartType enginePartType =
                            (carPartElement.CarPartType as CarPartTypeEngine).EnginePartType;
                        if (_enginePartsContainer.ContainsKey(enginePartType))
                        {
                            _enginePartsContainer[enginePartType].Add(carPartElement);
                        }
                        else
                        {
                            _enginePartsContainer.Add(enginePartType, new List<CarPartCompositeElement>()
                                { carPartElement });
                        }
                        break;

                    case CarSystem.Consumables:
                        ConsumablesPartType consumablesPartType =
                            (carPartElement.CarPartType as CarPartTypeConsumables).ConsumablesPartType;
                        if (_consumablesPartsContainer.ContainsKey(consumablesPartType))
                        {
                            _consumablesPartsContainer[consumablesPartType].Add(carPartElement);
                        }
                        else
                        {
                            _consumablesPartsContainer.Add(consumablesPartType, new List<CarPartCompositeElement>()
                                { carPartElement });
                        }
                        break;
                }
            }
        }

        private void FillConsumablesOriginalsContainer()
        {
            foreach (PickableCarPartCompositeElement pickableCarPart in _consumablesOriginals)
            {
                _consumablesOriginalsContainer
                    .Add((pickableCarPart.CarPartType as CarPartTypeConsumables).ConsumablesPartType, pickableCarPart);
            }
        }

        private void InitializeCarPartCompositeElements()
        {
            foreach (PickableCarPartCompositeElement carPartCompositeElement in _allPickableCarPartElements)
            {
                carPartCompositeElement.Initialize(GetCarPartElement(carPartCompositeElement.CarPartType));
            }
        }

        private List<CarPartCompositeElement> GetCarPartElement(CarPartType carPartType)
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

#if UNITY_EDITOR
        [ContextMenu("Fill All Pickable Car Part Elements List")]
        private void FillAllPickableCarPartElementsList()
        {
            StageHandle stage = StageUtility.GetCurrentStageHandle();
            _allPickableCarPartElements = stage.FindComponentsOfType<PickableCarPartCompositeElement>().ToList();
        }

        [ContextMenu("Fill All Car Part Elements List")]
        private void FillAllCarPartElementsList()
        {
            StageHandle stage = StageUtility.GetCurrentStageHandle();
            _allCompositeElements = stage.FindComponentsOfType<CarPartCompositeElement>().ToList();
        }
#endif
    }
}
