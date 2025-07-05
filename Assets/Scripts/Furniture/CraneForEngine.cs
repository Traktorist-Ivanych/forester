using DG.Tweening;
using UnityEngine;

public class CraneForEngine : MonoBehaviour
{
    [SerializeField] private Transform _armTransform;
    [SerializeField] private Transform _hydraulicBodyTransform;
    [SerializeField] private Transform _piston1Transform;
    [SerializeField] private Transform _piston2Transform;

    [Header("Settings")]
    [SerializeField] private int _iterationsNumber = 8;

    [Space]

    [SerializeField] private float _armAngleMin = -50;
    [SerializeField] private float _armAngleMax = 35;
    [SerializeField] private float _hydraulicBodyAngleMin = -25;
    [SerializeField] private float _hydraulicBodyAngleMax = -15;
    [SerializeField] private float _piston1YPositionMin = 0.07f;
    [SerializeField] private float _piston1YPositionMax = 0.36f;
    [SerializeField] private float _piston2YPositionMin = 0.04f;
    [SerializeField] private float _piston2YPositionMax = 0.33f;

    private int _currentIteration = 5;

    void Start()
    {
        Vector3 targetArmRotation = GetTargetRotation(_armAngleMin, _armAngleMax);
        _armTransform.localRotation = Quaternion.Euler(targetArmRotation);

        Vector3 targetHydraulicBodyRotation = GetTargetRotation(_hydraulicBodyAngleMin, _hydraulicBodyAngleMax);
        _hydraulicBodyTransform.localRotation = Quaternion.Euler(targetHydraulicBodyRotation);

        Vector3 targetHydraulicPiston1Position = GetTargetPosition(_piston1YPositionMin, _piston1YPositionMax);
        _piston1Transform.localPosition = targetHydraulicPiston1Position;

        Vector3 targetHydraulicPiston2Position = GetTargetPosition(_piston2YPositionMin, _piston2YPositionMax);
        _piston2Transform.localPosition = targetHydraulicPiston2Position;
    }

    public bool TryPutUp(float animationDuration)
    {
        if (_currentIteration < _iterationsNumber)
        {
            _currentIteration++;

            PlayAnimation(animationDuration);

            return true;
        }

        return false;
    }

    public bool TryPutDown(float animationDuration)
    {
        if (_currentIteration > 0)
        {
            _currentIteration--;

            PlayAnimation(animationDuration);

            return true;
        }

        return false;
    }

    private void PlayAnimation(float animationDuration)
    {
        Vector3 targetArmRotation = GetTargetRotation(_armAngleMin, _armAngleMax);
        _armTransform.DOLocalRotate(targetArmRotation, animationDuration).SetEase(Ease.InOutSine);

        Vector3 targetHydraulicBodyRotation = GetTargetRotation(_hydraulicBodyAngleMin, _hydraulicBodyAngleMax);
        _hydraulicBodyTransform.DOLocalRotate(targetHydraulicBodyRotation, animationDuration).SetEase(Ease.InOutSine);

        Vector3 targetHydraulicPiston1Position = GetTargetPosition(_piston1YPositionMin, _piston1YPositionMax);
        _piston1Transform.DOLocalMove(targetHydraulicPiston1Position, animationDuration).SetEase(Ease.InOutSine);

        Vector3 targetHydraulicPiston2Position = GetTargetPosition(_piston2YPositionMin, _piston2YPositionMax);
        _piston2Transform.DOLocalMove(targetHydraulicPiston2Position, animationDuration).SetEase(Ease.InOutSine);
    }

    private Vector3 GetTargetRotation(float min, float max)
    {
        return new Vector3(
                    x: 0,
                    y: 0,
                    z: min + (max - min) / _iterationsNumber * _currentIteration);
    }

    private Vector3 GetTargetPosition(float min, float max)
    {
        return new Vector3(
                    x: 0,
                    y: min + (max - min) / _iterationsNumber * _currentIteration,
                    z: 0);
    }
}
