using UnityEngine;

namespace Tools
{
    public class WrenchToolKit : ToolLeftMouseButtonPickable
    {
        [SerializeField] private WrenchPlayer _wrenchPlayer;
        [SerializeField] private Transform _wrenchPlayerParent;

        [SerializeField] private MeshRenderer _wrenchRenderer;

        private bool _isInPlayer;

        public override Tool GetTool()
        {
            _isInPlayer = true;
            _wrenchRenderer.enabled = false;
            _wrenchPlayer.gameObject.SetActive(true);
            return _wrenchPlayer;
        }

        public override void OnLeftMouseClicked()
        {
            if (_isInPlayer)
            {
                _isInPlayer = false;
                _wrenchRenderer.enabled = true;
                _wrenchPlayer.transform.SetParent(_wrenchPlayerParent, false);
                _wrenchPlayer.gameObject.SetActive(false);
            }
        }
    }
}
