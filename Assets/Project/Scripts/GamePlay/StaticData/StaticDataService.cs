using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.GamePlay.Armaments.Configs;
using Project.Scripts.GamePlay.Collection.Configs;
using Project.Scripts.GamePlay.Player.Abilities.Configs;
using Project.Scripts.GamePlay.Player.Inventory.Configs;
using Project.Scripts.GamePlay.Stats.Configs;
using Project.Scripts.GamePlay.Statuses;
using Project.Scripts.GamePlay.Windows;
using Project.Scripts.GamePlay.Windows.Configs;
using UnityEngine;

namespace Project.Scripts.GamePlay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WindowId, GameObject> _windowPrefabsById;
        private PlayerStartInventory _playerStartInventory;

        private PlayerStartAbilities _playerStartAbilities;
        private AllCollectableAbilities _allCollectableAbilities;
        private ArmamentsConfig _armamentsConfig;
        private IndicatorsConfig _indicatorsConfig;

        private StatIconsConfig _statIconsConfig;
        private StatusIconsConfig _statusIconsConfig;
        
        //private PlayerMovementConfig _playerMovementConfig;

        public void LoadAll()
        {
            LoadWindows();
            LoadAbilitiesConfig();
            LoadStartInventoryConfig();
            LoadProjectilesConfig();
            LoadAllCollectableAbilitiesConfig();
            LoadIndicatorsConfig();
            LoadIconsConfigs();
        }

        private void LoadIconsConfigs()
        {
            _statIconsConfig= Resources
                .Load<StatIconsConfig>("Configs/Icons/StatIconsConfig");
            _statusIconsConfig= Resources
                .Load<StatusIconsConfig>("Configs/Icons/StatusIconsConfig");
            
        }


        //public PlayerMovementConfig GetPlayerMovementConfig() => _playerMovementConfig;

        public PlayerStartInventory GetPlayerStartInventory() =>
            _playerStartInventory ?? throw new Exception("Player start inventory config was not loaded");
        public AllCollectableAbilities GetAllCollectableAbilities() =>
            _allCollectableAbilities ?? throw new Exception("All collectable abilities config was not loaded");

        public PlayerStartAbilities GetPlayerStartAbilities() =>
            _playerStartAbilities ?? throw new Exception("Player start abilities config was not loaded");
        public ArmamentsConfig GetProjectilesConfig() =>
            _armamentsConfig ?? throw new Exception("Projectiles config was not loaded");
        public IndicatorsConfig GetIndicatorsConfig() =>
            _indicatorsConfig ?? throw new Exception("Indicators config was not loaded");
        public StatIconsConfig GetStatIconsConfig() =>
            _statIconsConfig ?? throw new Exception("Stat icons config was not loaded");
        public StatusIconsConfig GetStatusIconsConfig() =>
            _statusIconsConfig ?? throw new Exception("Status icons config was not loaded");

        public GameObject GetWindowPrefab(WindowId id) =>
            _windowPrefabsById.TryGetValue(id, out GameObject prefab)
                ? prefab
                : throw new Exception($"Prefab config for window {id} was not found");


        private void LoadProjectilesConfig()
        {
            _armamentsConfig= Resources
                .Load<ArmamentsConfig>("Configs/Armaments/ProjectilesConfig");
        }

        private void LoadIndicatorsConfig()
        {
            _indicatorsConfig = Resources
                .Load<IndicatorsConfig>("Configs/Player/Abilities/IndicatorsConfig");
        }

        private void LoadAllCollectableAbilitiesConfig()
        {
            _allCollectableAbilities= Resources
                .Load<AllCollectableAbilities>("Configs/Collectables/AllCollectableAbilities");
        }

        private void LoadStartInventoryConfig()
        {
            _playerStartInventory= Resources
                .Load<PlayerStartInventory>("Configs/Player/Inventory/PlayerStartInventory");
        }

        private void LoadAbilitiesConfig()
        {
            _playerStartAbilities = Resources
                .Load<PlayerStartAbilities>("Configs/Player/Abilities/PlayerStartAbilities");
        }

        private void LoadWindows()
        {
            _windowPrefabsById = Resources
                .Load<WindowsConfig>("Configs/Windows/WindowsConfig")
                .WindowConfigs
                .ToDictionary(x => x.Id, x => x.Prefab);
        }

  

 
    }
}