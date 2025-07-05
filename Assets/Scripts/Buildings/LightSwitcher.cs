using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using UnityEngine;

namespace Buildings
{
    public class LightSwitcher : MonoBehaviour, ILeftMouseClickable
    {
        [SerializeField] private Transform _switcherTransform;
        [SerializeField] private float _rotationAngle = 5;
        [SerializeField] private List<PointChandelier> _spotChandeliers;

        private bool _isOn;

        private void Awake()
        {
            _switcherTransform.localRotation = Quaternion.Euler(new Vector3(-_rotationAngle, 0, 0));
        }

        public void OnLeftMouseClicked()
        {
            _isOn = !_isOn;

            Vector3 targetRotation = new Vector3(_isOn ? _rotationAngle : -_rotationAngle, 0, 0);

            _switcherTransform.DOKill();
            _switcherTransform.DOLocalRotate(targetRotation, 0.25f);


            foreach (PointChandelier spotChandelier in _spotChandeliers)
            {
                spotChandelier.Turn(_isOn);
            }
        }
    }
}
