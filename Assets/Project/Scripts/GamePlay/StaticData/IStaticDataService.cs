using Assets.Code.GamePlay.Abilities.Configs;
using Assets.Code.GamePlay.Armaments.Projectiles.Configs;
using Assets.Code.GamePlay.Player.Inventory.Configs;
using Code.Gameplay.Windows;
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
    }
}