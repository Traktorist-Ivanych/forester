using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "VisualEffectsConfig", menuName = "Configs/VisualEffectsConfig")]
    public class VisualEffectsConfig : ScriptableObject
    {
        [field: SerializeField] public Material ClickableOutlineMaskMaterial { get; private set; }
        [field: SerializeField] public Material ClickableOutlineFillMaterial { get; private set; }
    }
}
