using UnityEngine;

namespace Fenix
{
    public class FenixPartCompositeElement : MonoBehaviour
    {
        private bool _isInstalled;

        public bool IsInstalled => _isInstalled;

        public void Install(CarPartCompositeElement carPartCompositeElement)
        {
            _isInstalled = carPartCompositeElement.IsInstalled;
            gameObject.SetActive(_isInstalled);
        }
    }
}
