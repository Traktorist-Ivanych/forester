using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class PointChandelier : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _chandelierMeshRenderer;
        [SerializeField] private Material _chandelierOnMaterial;
        [SerializeField] private Material _chandelierOffMaterial;
        [SerializeField] private List<Light> _pointLights;

        void Awake()
        {
            Turn(false);
        }

        public void Turn(bool _isOn)
        {
            _chandelierMeshRenderer.material = _isOn ? _chandelierOnMaterial : _chandelierOffMaterial;

            foreach (Light pointLight in _pointLights)
            {
                pointLight.enabled = _isOn;
            }
        }
    }
}
