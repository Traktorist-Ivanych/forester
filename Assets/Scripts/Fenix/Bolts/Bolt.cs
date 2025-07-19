using System.Collections;
using DG.Tweening;
using Interfaces;
using Tools;
using UnityEngine;

namespace Fenix
{
    public abstract class Bolt : MonoBehaviour, IMouseScrollTool
    {
        [SerializeField] protected ToolType _toolType;
        [SerializeField] protected Transform _animatedBoltTransform;

        [SerializeField] protected GameObject _redBolt;
        [SerializeField] protected GameObject _greenBolt;

        protected int _tightening;
        protected float _startLocalPositionY;
        protected Tween _animationTween;

        protected const int TIGHTENING_MAX = 6;

        protected virtual void Awake()
        {
            _startLocalPositionY = _animatedBoltTransform.transform.localPosition.y;
        }

        public bool IsBoltUnscrewed()
        {
            return _tightening <= 0;
        }

        public bool IsBoltScrewed()
        {
            return _tightening >= TIGHTENING_MAX;
        }

        public void ShowRedBolt()
        {
            if (_redBolt.activeInHierarchy) return;

            StartCoroutine(ShowRedBoltCoroutine());
        }

        public void ShowGreenBolt()
        {
            if (_greenBolt.activeInHierarchy) return;

            _greenBolt.SetActive(true);
        }

        public void HideGreenBolt()
        {
            if (!_greenBolt.activeInHierarchy) return;

            _greenBolt.SetActive(false);
        }

        public bool TryUseMouseScrollTool(Vector2 input, Tool tool)
        {
            if (_animationTween != null) return false;

            if (CanInteract(tool, out int interactionSpeed))
            {
                return TryInteract(input, interactionSpeed);
            }

            return false;
        }

        protected virtual bool CanInteract(Tool tool, out int interactionSpeed)
        {
            interactionSpeed = 1;

            return tool.ToolType == _toolType;
        }

        private bool TryInteract(Vector2 input, int interactionSpeed)
        {
            if (input.y > 0)
            {
                if (_tightening < TIGHTENING_MAX)
                {
                    _tightening += interactionSpeed;

                    if (_tightening >= TIGHTENING_MAX)
                    {
                        HideGreenBolt();
                        _tightening = TIGHTENING_MAX;
                    }

                    AnimateBolt(interactionSpeed);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (input.y < 0)
            {
                if (_tightening > 0)
                {
                    _tightening -= interactionSpeed;

                    ShowGreenBolt();

                    if (_tightening < 0) _tightening = 0;

                    AnimateBolt(-interactionSpeed);

                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        private void AnimateBolt(int direction)
        {
            float currentYPosition = _startLocalPositionY - _tightening * (_startLocalPositionY / TIGHTENING_MAX);
            Vector3 currentRotationOffset = new Vector3(0, 720 / TIGHTENING_MAX * direction, 0);

            _animationTween = _animatedBoltTransform.DOLocalMoveY(currentYPosition, 0.25f)
                .OnComplete(() => _animationTween = null);
            _animatedBoltTransform.DOLocalRotate(currentRotationOffset, 0.25f, RotateMode.LocalAxisAdd);
        }

        private IEnumerator ShowRedBoltCoroutine()
        {
            _redBolt.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            _redBolt.SetActive(false);
        }
    }
}
