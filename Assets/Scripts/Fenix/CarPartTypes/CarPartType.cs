using UnityEngine;

namespace Fenix
{
    public abstract class CarPartType : MonoBehaviour
    {
        [SerializeField] private CarSystem carSystem;

        public CarSystem CarSystem => carSystem;
    }
}
