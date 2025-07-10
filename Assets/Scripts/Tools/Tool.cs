using Interfaces;
using UnityEngine;

namespace Tools
{
    public abstract class Tool : MonoBehaviour
    {
        [SerializeField] private ToolType _toolType;

        public ToolType ToolType => _toolType;

        public virtual Tool GetTool()
        {
            return this;
        }
    }

    public enum ToolType
    {
        None,
        WrenchDefault
    }
}
