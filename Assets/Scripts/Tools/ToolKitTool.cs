using UnityEngine;

namespace Tools
{
    public class ToolKitTool : ToolLeftMouseButtonPickable
    {
        [SerializeField] private Tool _playerTool;
        [SerializeField] private Transform _playerToolParent;

        [SerializeField] private MeshRenderer _toolKitToolRenderer;

        private bool _isInPlayer;

        public override Tool GetTool()
        {
            _isInPlayer = true;
            _toolKitToolRenderer.enabled = false;
            _playerTool.gameObject.SetActive(true);
            return _playerTool;
        }

        public override void OnLeftMouseClicked()
        {
            if (_isInPlayer)
            {
                _isInPlayer = false;
                _toolKitToolRenderer.enabled = true;
                _playerTool.transform.SetParent(_playerToolParent, false);
                _playerTool.gameObject.SetActive(false);
            }
        }
    }
}
