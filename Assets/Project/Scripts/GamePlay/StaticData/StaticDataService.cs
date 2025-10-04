using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.GamePlay.Abilities.Configs;
using Assets.Code.GamePlay.Armaments.Projectiles.Configs;
using Assets.Code.GamePlay.Player.Inventory.Configs;
using Code.Gameplay.Windows;
using Code.Gameplay.Windows.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WindowId, GameObject> _windowPrefabsById;
        private PlayerStartInventory _playerStartInventory;

        private PlayerStartAbilities _playerStartAbilities;
        private ArmamentsConfig _armamentsConfig;
        //private PlayerMovementConfig _playerMovementConfig;

        public void LoadAll()
        {
            LoadWindows();
            LoadAbilitiesConfig();
            LoadStartInventoryConfig();
            LoadProjectilesConfig();
        }


        //public PlayerMovementConfig GetPlayerMovementConfig() => _playerMovementConfig;

        public PlayerStartInventory GetPlayerStartInventory() =>
            _playerStartInventory ?? throw new Exception("Player start inventory config was not loaded");

        public PlayerStartAbilities GetPlayerStartAbilities() =>
            _playerStartAbilities ?? throw new Exception("Player start abilities config was not loaded");
        public ArmamentsConfig GetProjectilesConfig() =>
            _armamentsConfig ?? throw new Exception("Projectiles config was not loaded");

        public GameObject GetWindowPrefab(WindowId id) =>
            _windowPrefabsById.TryGetValue(id, out GameObject prefab)
                ? prefab
                : throw new Exception($"Prefab config for window {id} was not found");


        private void LoadProjectilesConfig()
        {
            _armamentsConfig= Resources
                .Load<ArmamentsConfig>("Configs/Armaments/ProjectilesConfig");
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