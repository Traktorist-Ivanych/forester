using UnityEngine;

namespace Fenix
{
    public abstract class CarPartType : MonoBehaviour
    {
        [SerializeField] protected CarSystem carSystem;

        public CarSystem CarSystem => carSystem;
    }
}
