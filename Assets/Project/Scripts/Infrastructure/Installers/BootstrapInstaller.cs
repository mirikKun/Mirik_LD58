using Assets.Code;
using Assets.Code.GamePlay.Abilities.Systems;
using Assets.Code.GamePlay.Armaments.Projectiles.Factories;
using Assets.Code.GamePlay.Common.GameBehaviour.Services;
using Assets.Code.GamePlay.Player.Abilities.Factory;
using Assets.Code.GamePlay.Player.Inventory;
using Code.Gameplay.Common.Random;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Levels;
using Code.Gameplay.StaticData;
using Code.Gameplay.Windows;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Progress.Provider;
using Zenject;

namespace Code.Infrastructure.Installers
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
        }

        private void BindStateMachine()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }

        private void BindStateFactory()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
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
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
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