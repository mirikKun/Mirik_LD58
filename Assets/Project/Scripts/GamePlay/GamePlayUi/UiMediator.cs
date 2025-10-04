using Assets.Code.GamePlay.Inventory.UI;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.GamePlayUi
{
    public class UiMediator : MonoBehaviour
    {
        [SerializeField] private InventoryUI _inventoryUI;


        private IInputReader _inputReader;
        private bool _isInventoryOpen;
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IInputReader inputReader, IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _inputReader = inputReader;
        }

        private void Start()
        {
            _inputReader.InventoryPressed += OnInventoryPressed;
        }

        private void OnDestroy()
        {
            _inputReader.InventoryPressed -= OnInventoryPressed;
        }

        private void OnInventoryPressed()
        {
            if (_isInventoryOpen)
            {
                CLoseInventory();
            }
            else
            {
                OpenInventory();
            }
        }

        private void OnUiClosed()
        {
            _gameStateMachine.Enter<GameLoopState>();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnUiOpened()
        {
            _gameStateMachine.Enter<PauseState>();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void OpenInventory()
        {
            _isInventoryOpen=true;
            _inventoryUI.InventoryUIRoot.gameObject.SetActive(true);
            OnUiOpened();
        }

        private void CLoseInventory()
        {
            _isInventoryOpen = false;
            _inventoryUI.InventoryUIRoot.gameObject.SetActive(false);
            OnUiClosed();
        }
    }
}