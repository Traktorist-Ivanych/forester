using UnityEngine;
using Zenject;

namespace ProjectInput
{
    public class ProjectInputInstaller : MonoInstaller
    {
        [SerializeField] private InputManager _inputManager;

        public override void InstallBindings()
        {
            Container.Bind<InputManager>().FromInstance(_inputManager).AsSingle().NonLazy();
        }
    }
}
