using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using Zenject;

namespace Fenix
{
    public class PickableCarPartCompositeElementsContainer : MonoBehaviour
    {
        [Inject] private readonly PickableCarPartsContainer _pickableCarPartsContainer;

        [SerializeField] private List<PickableCarPartCompositeElement> _allPickableCarPartElements;

        private Dictionary<EnginePartType, List<PickableCarPartCompositeElement>> _enginePartsContainer = new();
        private Dictionary<ConsumablesPartType, List<PickableCarPartCompositeElement>> _consumablesPartsContainer = new();

        private void Awake()
        {
            FillContainers();
        }

        public void InitializeCarPartCompositeElements()
        {
            foreach (PickableCarPartCompositeElement carPartCompositeElement in _allPickableCarPartElements)
            {
                carPartCompositeElement.Initialize(
                    _pickableCarPartsContainer.GetPickableCarParts(carPartCompositeElement.ParentCarPartType));
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
                        if (_enginePartsContainer.ContainsKey(enginePartType))
                        {
                            _enginePartsContainer[enginePartType].Add(carPartElement);
                        }
                        else
                        {
                            _enginePartsContainer.Add(enginePartType, new List<PickableCarPartCompositeElement>()
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
                            _consumablesPartsContainer.Add(consumablesPartType, new List<PickableCarPartCompositeElement>()
                                { carPartElement });
                        }
                        break;
                }
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Fill All Pickable Car Part Elements List")]
        private void FillAllPickableCarPartElementsList()
        {
            StageHandle stage = StageUtility.GetCurrentStageHandle();
            _allPickableCarPartElements = stage.FindComponentsOfType<PickableCarPartCompositeElement>().ToList();
        }
#endif
    }
}
