using DG.Tweening;
using Interfaces;
using UnityEngine;

public class CraneForEngineHandle : MonoBehaviour, ILeftMouseClickable, IRightMouseClickable
{
    [Header("Links")]
    [SerializeField] private CraneForEngine _craneForEngine;
    [SerializeField] private Transform _handleTransform;

    [Header("Settings")]
    [SerializeField] private float _handleActiveAngle = 45;
    [SerializeField] private float _handleDefaultAngle = -60;
    [SerializeField] private float _animationDuration = 0.25f;

    private Vector3 _defaultRotation;
    private bool _isAnimationsPlaying;

    void Start()
    {
        _defaultRotation = new Vector3(0, 0, _handleDefaultAngle);
        _handleTransform.localRotation = Quaternion.Euler(_defaultRotation);
    }

    public void OnLeftMouseClicked()
    {
        if (!_isAnimationsPlaying)
        {
            _isAnimationsPlaying = true;

            if (_craneForEngine.TryPutUp(_animationDuration))
            {
                PlayActiveAnimation(-_handleActiveAngle, Ease.InOutSine);
            }
            else
            {
                PlayActiveAnimation(-_handleActiveAngle / 3, Ease.OutBounce);
            }
        }
    }

    public void OnRightMouseClicked()
    {
        if (!_isAnimationsPlaying)
        {
            _isAnimationsPlaying = true;

            if (_craneForEngine.TryPutDown(_animationDuration))
            {
                PlayActiveAnimation(_handleActiveAngle, Ease.InOutSine);
            }
            else
            {
                PlayActiveAnimation(_handleActiveAngle / 3, Ease.OutBounce);
            }
        }
    }

    private void PlayActiveAnimation(float angle, Ease ease)
    {
        Sequence handleSequence = DOTween.Sequence();

        Vector3 activeRotation = new Vector3(
            x: 0,
            y: 0,
            z: _handleDefaultAngle + angle);

        handleSequence.Append(_handleTransform.DOLocalRotate(activeRotation, _animationDuration)
            .SetEase(ease));
        handleSequence.Append(_handleTransform.DOLocalRotate(_defaultRotation, _animationDuration)
            .SetEase(Ease.InOutSine));
        handleSequence.OnComplete(() => _isAnimationsPlaying = false);
    }
}
