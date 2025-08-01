using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Zenject;

namespace Fenix
{
    public class FenixPart : MonoBehaviour, IRightMouseClickable
    {
        [Inject] protected readonly PickableCarPartsContainer _pickableCarPartsContainer;

        [SerializeField] private CarPartType _carPartType;

        [SerializeField] private List<FenixPart> _nextParts;
        [SerializeField] private List<FenixPart> _previousParts;

        [SerializeField] private GameObject _fenixPartObject;
        [SerializeField] private List<Bolt> _bolts;

        [SerializeField] private GameObject _redPart;

        protected IPickableItemCarPart _pickableItemCarPart;
        protected bool _isInstalled;

        public CarPartType CarPartType => _carPartType;
        public bool IsInstalled => _isInstalled;

        private void OnValidate()
        {
            if (_carPartType == null)
            {
                TryGetComponent(out _carPartType);
            }
        }

        public bool CanBeInstalled()
        {
            if (_previousParts.Count <= 0) return true;

            foreach (FenixPart previousPart in _previousParts)
            {
                if (!previousPart.IsInstalled) return false;
            }

            return true;
        }

        public void ShowRedPart()
        {
            if (_redPart.activeInHierarchy) return;

            StartCoroutine(ShowRedPartCoroutine());
        }

        public void Install(IPickableItemCarPart pickableItemCarPart)
        {
            _fenixPartObject.SetActive(true);

            foreach (Bolt bolt in _bolts)
            {
                bolt.ShowGreenBolt();
            }

            _isInstalled = true;
        }

        public void OnRightMouseClicked()
        {
            Dismantle();
        }

        protected virtual void Dismantle()
        {
            if (!_isInstalled) return;

            bool canBeDismantle = true;

            foreach (Bolt bolt in _bolts)
            {
                if (!bolt.IsBoltUnscrewed())
                {
                    bolt.ShowRedBolt();
                    canBeDismantle = false;
                }
            }

            if (!canBeDismantle) return;

            foreach (FenixPart nextPart in _nextParts)
            {
                if (nextPart.IsInstalled)
                {
                    nextPart.ShowRedPart();
                    canBeDismantle = false;
                }
            }

            if (!canBeDismantle) return;

            _isInstalled = false;

            _fenixPartObject.SetActive(false);

            _pickableItemCarPart = _pickableCarPartsContainer.GetPickableCarPart(_carPartType);
            _pickableItemCarPart.Dismantle(transform);
        }

        private IEnumerator ShowRedPartCoroutine()
        {
            _redPart.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            _redPart.SetActive(false);
        }
    }
}
