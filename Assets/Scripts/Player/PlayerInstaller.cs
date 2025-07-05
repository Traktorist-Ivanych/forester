using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerMovementCC _playerMovementCC;
        [SerializeField] private PlayerInputManager _playerRaycast;

        public override void InstallBindings()
        {
            Container.Bind<PlayerMovementCC>().FromInstance(_playerMovementCC).AsSingle().NonLazy();
            Container.Bind<PlayerInputManager>().FromInstance(_playerRaycast).AsSingle().NonLazy();
        }
    }
}
