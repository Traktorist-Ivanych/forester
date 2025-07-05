using UnityEngine;
using Zenject;

namespace Configs
{
    public class ConfigsInstaller : MonoInstaller
    {
        [SerializeField] private VisualEffectsConfig _visualEffectsConfig;

        public override void InstallBindings()
        {
            Container.Bind<VisualEffectsConfig>().FromInstance(_visualEffectsConfig).AsSingle().NonLazy();
        }
    }
}
