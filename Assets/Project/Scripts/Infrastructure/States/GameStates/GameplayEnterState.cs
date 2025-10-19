using System.Collections.Generic;
using System.Linq;
using Project.Scripts.GamePlay.Collection.Configs;
using Project.Scripts.GamePlay.Collection.Systems;
using Project.Scripts.GamePlay.Level.Systems;
using Project.Scripts.GamePlay.Player.Abilities.Configs;
using Project.Scripts.GamePlay.Player.Abilities.Systems;
using Project.Scripts.GamePlay.Player.Inventory.General;
using Project.Scripts.GamePlay.Player.Inventory.Systems;
using Project.Scripts.GamePlay.StaticData;
using Project.Scripts.Infrastructure.States.StateInfrastructure;
using Project.Scripts.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Project.Scripts.Infrastructure.States.GameStates
{
    public class GameplayEnterState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ILevelDataProvider _levelDataProvider;

        private readonly IInventorySystem _inventorySystem;
        private readonly IStaticDataService _staticDataService;
        private readonly IAbilitiesSystem _abilitiesSystem;
        private ICollectionSystem _collectionSystem;


        public GameplayEnterState(IGameStateMachine stateMachine, ILevelDataProvider levelDataProvider,
            IInventorySystem inventorySystem,IStaticDataService staticDataService,IAbilitiesSystem abilitiesSystem,ICollectionSystem collectionSystem)
        {
            _collectionSystem = collectionSystem;
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
            SetupCollector();
            SetupAbilities();
            _stateMachine.Enter<GameLoopState>();
        }

        private void SetupCollector()
        {
            AllCollectableAbilities allCollectableAbilities= _staticDataService.GetAllCollectableAbilities();
            _collectionSystem.Setup(allCollectableAbilities);
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