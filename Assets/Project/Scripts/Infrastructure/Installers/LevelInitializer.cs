using Assets.Code.GamePlay.Armaments.Projectiles.Factories;
using Code.Gameplay.Levels;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class LevelInitializer : MonoBehaviour, IInitializable
    {
        [SerializeField] private CinemachineCamera _mainCamera;
        [SerializeField] private Transform _levelGeneratorParent;
        [SerializeField] private Transform _playerSpawnPoint;
        [Header("Factories")]
        [SerializeField] private Transform _projectileSpawnParent;
        private ILevelDataProvider _levelDataProvider;
        private IArmamentsFactory _armamentsFactory;

        [Inject]
        private void Construct( ILevelDataProvider levelDataProvider,IArmamentsFactory armamentsFactory)
        {
            _armamentsFactory = armamentsFactory;
            _levelDataProvider = levelDataProvider;
        }

        public void Initialize()
        {
            _levelDataProvider.SetStartPoint(_playerSpawnPoint);
            _levelDataProvider.SetLevelGeneratorTransform(_levelGeneratorParent);
            _levelDataProvider.SetCamera(_mainCamera);
            
            _armamentsFactory.SetupArmamentsParent(_projectileSpawnParent);
        }
    }
}