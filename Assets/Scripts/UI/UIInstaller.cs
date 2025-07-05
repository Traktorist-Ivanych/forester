using UnityEngine;
using Zenject;

namespace UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private HUDCanvas _hUDCanvas;

        public override void InstallBindings()
        {
            Container.Bind<HUDCanvas>().FromInstance(_hUDCanvas).AsSingle().NonLazy();
        }
    }
}
