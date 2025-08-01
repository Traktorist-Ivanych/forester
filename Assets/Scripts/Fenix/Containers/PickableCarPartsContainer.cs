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

        [SerializeField] private List<PickableCarPart> _allPickableCarParts;

        [SerializeField] private List<PickableCarPart> _consumablesOriginals;

        private Dictionary<EnginePartType, List<PickableCarPart>> _enginePartsContainer = new();
        private Dictionary<ConsumablesPartType, List<PickableCarPart>> _consumablesPartsContainer = new();

        private Dictionary<ConsumablesPartType, PickableCarPart> _consumablesOriginalsContainer = new();

        private void Awake()
        {
            FillContainers();

            FillConsumablesOriginalsContainer();
        }

        public void InitializePickableCarParts()
        {
            foreach (PickableCarPart pickableCarPart in _allPickableCarParts)
            {
                pickableCarPart.Initialize(_fenixPartsContainer.GetFenixParts(pickableCarPart.CarPartType));
            }
        }

        public PickableCarPart GetPickableCarPart(CarPartType carPartType)
        {
            switch (carPartType.CarSystem)
            {
                case CarSystem.Engine:
                    EnginePartType enginePartType = (carPartType as CarPartTypeEngine).EnginePartType;
                    foreach (PickableCarPart pickableCarPart in _enginePartsContainer[enginePartType])
                    {
                        if (!pickableCarPart.gameObject.activeInHierarchy)
                        {
                            return pickableCarPart;
                        }
                    }

                    Debug.LogError("Not enough unactive PickableCarPart!");
                    return null;

                case CarSystem.Consumables:
                    ConsumablesPartType consumablesPartType = (carPartType as CarPartTypeConsumables).ConsumablesPartType;
                    foreach (PickableCarPart pickableCarPart in _consumablesPartsContainer[consumablesPartType])
                    {
                        if (!pickableCarPart.gameObject.activeInHierarchy)
                        {
                            return pickableCarPart;
                        }
                    }

                    PickableCarPart consumablesCopy = Instantiate(_consumablesOriginalsContainer[consumablesPartType]);
                    _consumablesPartsContainer[consumablesPartType].Add(consumablesCopy);

                    return consumablesCopy;

                default:
                    Debug.LogError($"PickableCarPartsContainer not contains CarSystem {carPartType.CarSystem}");
                    return null;
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

        private void FillConsumablesOriginalsContainer()
        {
            foreach (PickableCarPart pickableCarPart in _consumablesOriginals)
            {
                _consumablesOriginalsContainer
                    .Add((pickableCarPart.CarPartType as CarPartTypeConsumables).ConsumablesPartType, pickableCarPart);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Fill All Pickable Car Parts List")]
        private void FillAllPickableCarParts()
        {
            StageHandle stage = StageUtility.GetCurrentStageHandle();
            _allPickableCarParts = stage.FindComponentsOfType<PickableCarPart>().ToList();

            for (int i = _allPickableCarParts.Count - 1; i >= 0; i--)
            {
                foreach (PickableCarPart consumablesOriginal in _consumablesOriginals)
                {
                    if (_allPickableCarParts[i].Equals(consumablesOriginal))
                    {
                        _allPickableCarParts.RemoveAt(i);
                        break;
                    }
                }
            }
        }
#endif
    }
}
