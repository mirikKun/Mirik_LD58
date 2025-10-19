using Project.Scripts.GamePlay.Armaments.Factories;
using Project.Scripts.GamePlay.Collection.Systems;
using Project.Scripts.GamePlay.Common.GameBehaviour.Services;
using Project.Scripts.GamePlay.Common.Input;
using Project.Scripts.GamePlay.Common.Random;
using Project.Scripts.GamePlay.Common.Time;
using Project.Scripts.GamePlay.Level.Systems;
using Project.Scripts.GamePlay.Player.Abilities.Factory;
using Project.Scripts.GamePlay.Player.Abilities.Systems;
using Project.Scripts.GamePlay.Player.Inventory.Systems;
using Project.Scripts.GamePlay.StaticData;
using Project.Scripts.GamePlay.Windows;
using Project.Scripts.Infrastructure.AssetManagement;
using Project.Scripts.Infrastructure.Loading;
using Project.Scripts.Infrastructure.Progress.Provider;
using Project.Scripts.Infrastructure.States.Factory;
using Project.Scripts.Infrastructure.States.GameStates;
using Project.Scripts.Infrastructure.States.StateMachine;
using Project.Scripts.Sounds;
using Zenject;

namespace Project.Scripts.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
    {
        public override void InstallBindings()
        {
            BindInputService();
            BindProgressServices();
            BindStateFactory();
            BindStateMachine();
            BindGameStates();
            BindGameplayFactories();
            BindUIServices();
            BindInfrastructureServices();
            BindAssetManagementServices();
            BindCommonServices();
            BindGameplayServices();
            BindGameplaySystems();
            BoundsSounds();
        }

        private void BindStateMachine()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }

        private void BindStateFactory()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
        }

        private void BoundsSounds()
        {
            Container.Bind<ISoundsSystem>().To<SoundsSystem>().AsSingle();

        }

        private void BindGameStates()
        {
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<InitializeProgressState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingMainMenuScreenState>().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuScreenState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingGameplayState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayEnterState>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoopState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverState>().AsSingle();
        }


        private void BindGameplaySystems()
        {
            Container.Bind<IUpdateService>().To<UpdateService>().AsSingle();
            Container.Bind<IInventorySystem>().To<InventorySystem>().AsSingle();
            Container.Bind<IAbilitiesSystem>().To<AbilitiesSystem>().AsSingle();
            Container.Bind<ICollectionSystem>().To<CollectionSystem>().AsSingle();
        }

        private void BindGameplayFactories()
        {
            Container.Bind<IArmamentsFactory>().To<ArmamentsFactory>().AsSingle();
            Container.Bind<IAbilitiesFactory>().To<AbilitiesFactory>().AsSingle();
            Container.Bind<IWindowFactory>().To<WindowFactory>().AsSingle();
        }

        private void BindGameplayServices()
        {
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<ILevelDataProvider>().To<LevelDataProvider>().AsSingle();
        }


        private void BindInfrastructureServices()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
        }

        private void BindAssetManagementServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }

        private void BindProgressServices()
        {
            Container.Bind<IProgressProvider>().To<ProgressProvider>().AsSingle();
        }

        private void BindUIServices()
        {
            Container.Bind<IWindowService>().To<WindowService>().AsSingle();
        }

        private void BindCommonServices()
        {
            Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
            Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
            Container.Bind<ISceneLoader>().To<Loading.SceneLoader>().AsSingle();
        }

        private void BindInputService()
        {
            Container.Bind<IInputReader>().To<InputReader>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
        }
    }
}