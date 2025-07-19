using Interfaces;
using UnityEngine;

namespace Tools
{
    public abstract class Tool : MonoBehaviour
    {
        [SerializeField] private ToolType _toolType;
        [SerializeField] private Vector3 _localPosition;
        [SerializeField] private Vector3 _localRotation;

        public ToolType ToolType => _toolType;

        public virtual Tool GetTool()
        {
            return this;
        }

        public void TakeInPlayerHands(Transform playerHandsTransform)
        {
            transform.SetParent(playerHandsTransform, false);
            transform.SetLocalPositionAndRotation(_localPosition, Quaternion.Euler(_localRotation));
        }
    }

    public enum ToolType
    {
        None,
        Wrench,
        OilFilterWrench,
        SparkPlugWrench,
        Screwdriver,
        FeelerGaugeSet
    }
}
