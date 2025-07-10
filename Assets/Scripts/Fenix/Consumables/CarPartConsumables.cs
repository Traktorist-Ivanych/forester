using UnityEngine;

namespace Fenix
{
    public class CarPartConsumables : MonoBehaviour
    {
        [SerializeField] protected float _healthPoints;

        public float HealthPoints => _healthPoints;

        public virtual void SetHealthPoints(float healthPoints)
        {
            _healthPoints = healthPoints;
        }

        public virtual void AddHealthPoints(float healthPoints)
        {
            _healthPoints += healthPoints;
        }

        public virtual void RemoveHealthPoints(float healthPoints)
        {
            _healthPoints -= healthPoints;

            if (_healthPoints < 0)
            {
                _healthPoints = 0;
            }
        }
    }
}
