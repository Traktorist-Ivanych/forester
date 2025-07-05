using DG.Tweening;
using Interfaces;
using UnityEngine;

public class ToolKitWrenchesLatch : MonoBehaviour, ILeftMouseClickable
{
    [SerializeField] private Transform _latchTransform;
    [SerializeField] private Transform _coverTransform;

    private bool _isOpen;
    private bool _isAnimationsPlaying;

    public void OnLeftMouseClicked()
    {
        if (_isAnimationsPlaying) return;

        if (_isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    private void Open()
    {
        _isAnimationsPlaying = true;

        _latchTransform.DOLocalRotate(new Vector3(0, 0, -90), 0.25f).SetEase(Ease.InCubic);
        _coverTransform.DOLocalRotate(new Vector3(0, 0, -135), 0.75f, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutSine)
            .SetDelay(0.25f)
            .OnComplete(() =>
                {
                    _isAnimationsPlaying = false;
                    _isOpen = true;
                });
    }

    private void Close()
    {
        _isAnimationsPlaying = true;

        _coverTransform.DOLocalRotate(new Vector3(0, 0, 135), 0.75f, RotateMode.LocalAxisAdd).SetEase(Ease.InOutSine);
        _latchTransform.DOLocalRotate(new Vector3(0, 0, 0), 0.25f).SetEase(Ease.InOutSine).SetDelay(0.75f)
            .OnComplete(() =>
                {
                    _isAnimationsPlaying = false;
                    _isOpen = false;
                });
    }
}
