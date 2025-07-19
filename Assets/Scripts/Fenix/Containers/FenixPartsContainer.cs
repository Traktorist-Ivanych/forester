using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using Zenject;

namespace Fenix
{
    public class FenixPartsContainer : MonoBehaviour
    {
        [Inject] private readonly PickableCarPartsContainer _pickableCarPartsContainer;

        [SerializeField] private List<FenixPart> _allFenixParts;

        private Dictionary<EnginePartType, List<FenixPart>> _enginePartsContainer = new();
        private Dictionary<ConsumablesPartType, List<FenixPart>> _consumablesPartsContainer = new();

        private void Awake()
        {
            FillContainers();

            _pickableCarPartsContainer.InitializePickableCarParts();
        }

        public List<FenixPart> GetFenixParts(CarPartType carPartType)
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
                    Debug.LogError($"FenixPartsContainer not contains CarSystem {carPartType.CarSystem}");
                    return null;
            }
        }

        private void FillContainers()
        {
            foreach (FenixPart fenixPart in _allFenixParts)
            {
                switch (fenixPart.CarPartType.CarSystem)
                {
                    case CarSystem.Engine:
                        EnginePartType enginePartType =
                            (fenixPart.CarPartType as CarPartTypeEngine).EnginePartType;
                        if (_enginePartsContainer.ContainsKey(enginePartType))
                        {
                            _enginePartsContainer[enginePartType].Add(fenixPart);
                        }
                        else
                        {
                            _enginePartsContainer.Add(enginePartType, new List<FenixPart>() { fenixPart });
                        }
                        break;

                    case CarSystem.Consumables:
                        ConsumablesPartType consumablesPartType =
                            (fenixPart.CarPartType as CarPartTypeConsumables).ConsumablesPartType;
                        if (_consumablesPartsContainer.ContainsKey(consumablesPartType))
                        {
                            _consumablesPartsContainer[consumablesPartType].Add(fenixPart);
                        }
                        else
                        {
                            _consumablesPartsContainer.Add(consumablesPartType, new List<FenixPart>() { fenixPart });
                        }
                        break;
                }
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Fill All Fenix Parts List")]
        private void FillAllFenixPartsList()
        {
            StageHandle stage = StageUtility.GetCurrentStageHandle();
            _allFenixParts = stage.FindComponentsOfType<FenixPart>().ToList();
        }
#endif
    }
}
