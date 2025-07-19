using UnityEngine;
using Zenject;

namespace Fenix
{
    public class FenixInstaller : MonoInstaller
    {

        [SerializeField] private FenixPartsContainer _fenixPartsContainer;
        [SerializeField] private PickableCarPartsContainer _pickableCarPartsContainer;
        [SerializeField] private PickableCarPartCompositeElementsContainer _compositeElementsContainer;

        public override void InstallBindings()
        {
            Container.Bind<FenixPartsContainer>().FromInstance(_fenixPartsContainer).AsSingle().NonLazy();
            Container.Bind<PickableCarPartsContainer>().FromInstance(_pickableCarPartsContainer).AsSingle().NonLazy();
            Container.Bind<PickableCarPartCompositeElementsContainer>()
                .FromInstance(_compositeElementsContainer).AsSingle().NonLazy();
        }
    }
}
