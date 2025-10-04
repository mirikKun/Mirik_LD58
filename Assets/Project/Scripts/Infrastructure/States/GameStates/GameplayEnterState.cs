using System.Collections.Generic;
using System.Linq;
using Assets.Code.GamePlay.Abilities.Configs;
using Assets.Code.GamePlay.Abilities.Systems;
using Assets.Code.GamePlay.Player.Inventory;
using Assets.Code.GamePlay.Player.Inventory.General;
using Assets.Code.GamePlay.Player.Inventory.Items;
using Code.Gameplay.Common.Time;
using Code.Gameplay.Levels;
using Code.Gameplay.StaticData;
using Code.Infrastructure.States.StateInfrastructure;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Code.Infrastructure.States.GameStates
{
    public class GameplayEnterState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ILevelDataProvider _levelDataProvider;

        private readonly IInventorySystem _inventorySystem;
        private readonly IStaticDataService _staticDataService;
        private readonly IAbilitiesSystem _abilitiesSystem;


        public GameplayEnterState(IGameStateMachine stateMachine, ILevelDataProvider levelDataProvider,
            IInventorySystem inventorySystem,IStaticDataService staticDataService,IAbilitiesSystem abilitiesSystem)
        {
            _stateMachine = stateMachine;
            _levelDataProvider = levelDataProvider;
            _inventorySystem = inventorySystem;
            _staticDataService = staticDataService;
            _abilitiesSystem = abilitiesSystem;
        }

        public void Enter()
        {
            SetupCursor();
            PlacePlayer();
            SetupCamera();
            SetupInventory();
            SetupAbilities();
            _stateMachine.Enter<GameLoopState>();
        }

        private void SetupInventory()
        {
            List<AbilitySlot> activeAbilities = _staticDataService.GetPlayerStartInventory().ActiveAbilities.ToList();
            List<IAbilityItem> inactiveAbilities = _staticDataService.GetPlayerStartInventory().InactiveAbilities.Cast<IAbilityItem>().ToList();
            _inventorySystem.SetupInventory(activeAbilities, inactiveAbilities);
            
            
        }

        private void SetupAbilities()
        {

           PlayerStartAbilities startAbilities= _staticDataService.GetPlayerStartAbilities();
            _abilitiesSystem.Setup(startAbilities);

        }

        public void SetupCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
     

        private void PlacePlayer()
        {
        
            
        }

        private void SetupCamera()
        {
        
        }

        public void Exit()
        {
        }
    }
}