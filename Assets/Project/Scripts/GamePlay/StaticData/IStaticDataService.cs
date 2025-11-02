using Project.Scripts.GamePlay.Armaments.Configs;
using Project.Scripts.GamePlay.Collection.Configs;
using Project.Scripts.GamePlay.Player.Abilities.Configs;
using Project.Scripts.GamePlay.Player.Inventory.Configs;
using Project.Scripts.GamePlay.Stats.Configs;
using Project.Scripts.GamePlay.Statuses;
using Project.Scripts.GamePlay.Windows;
using UnityEngine;

namespace Project.Scripts.GamePlay.StaticData
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
        IndicatorsConfig GetIndicatorsConfig();
    }
}