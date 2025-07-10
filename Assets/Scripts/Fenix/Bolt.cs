using System.Collections;
using DG.Tweening;
using Interfaces;
using Tools;
using UnityEngine;

namespace Fenix
{
    public class Bolt : MonoBehaviour, IMouseScrollTool
    {
        [SerializeField] private int _boltSize;
        [SerializeField] private Transform _animatedBoltTransform;

        [SerializeField] private GameObject _redBolt;
        [SerializeField] private GameObject _greenBolt;

        private int _tightening;
        private float _startLocalPositionY;
        private Tween _animationTween;

        private const int TIGHTENING_MAX = 6;

        private void Awake()
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

            if (tool.ToolType == ToolType.WrenchDefault)
            {
                WrenchPlayer wrenchPlayer = tool as WrenchPlayer;

                if (_boltSize == wrenchPlayer.Size)
                {
                    if (input.y > 0)
                    {
                        if (_tightening < TIGHTENING_MAX)
                        {
                            _tightening += wrenchPlayer.InteractionSpeed;

                            if (_tightening >= TIGHTENING_MAX)
                            {
                                HideGreenBolt();
                                _tightening = TIGHTENING_MAX;
                            }
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
                            _tightening -= wrenchPlayer.InteractionSpeed;

                            ShowGreenBolt();

                            if (_tightening < 0) _tightening = 0;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    float currentYPosition = _startLocalPositionY - _tightening * (_startLocalPositionY / TIGHTENING_MAX);
                    Vector3 currentLocalRotation = new Vector3(0, 720 / TIGHTENING_MAX * _tightening, 0);

                    _animationTween = _animatedBoltTransform.DOLocalMoveY(currentYPosition, 0.25f)
                        .OnComplete(() => _animationTween = null);
                    _animatedBoltTransform.DOLocalRotate(currentLocalRotation, 0.25f, RotateMode.FastBeyond360);

                    return true;
                }
            }

            return false;
        }

        private IEnumerator ShowRedBoltCoroutine()
        {
            _redBolt.SetActive(true);
            yield return new WaitForSeconds(0.25f);
            _redBolt.SetActive(false);
        }
    }
}
