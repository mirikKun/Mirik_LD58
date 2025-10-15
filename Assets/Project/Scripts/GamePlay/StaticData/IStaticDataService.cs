using Assets.Code.GamePlay.Abilities.Configs;
using Assets.Code.GamePlay.Player.Inventory.Configs;
using Assets.Code.GamePlay.Stats;
using Code.Gameplay.Windows;
using Project.Scripts.GamePlay.Armaments.Configs;
using Project.Scripts.GamePlay.Collection.Configs;
using Project.Scripts.GamePlay.Statuses;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        GameObject GetWindowPrefab(WindowId id);

        //PlayerMovementConfig GetPlayerMovementConfig();
        PlayerStartInventory GetPlayerStartInventory();
        PlayerStartAbilities GetPlayerStartAbilities();
        ArmamentsConfig GetProjectilesConfig();
        AllCollectableAbilities GetAllCollectableAbilities();
        StatIconsConfig GetStatIconsConfig();
        StatusIconsConfig GetStatusIconsConfig();
    }
}