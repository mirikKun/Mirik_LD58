using Project.Scripts.Infrastructure.Progress.Provider;
using Project.Scripts.Infrastructure.States.GameStates;
using Project.Scripts.Infrastructure.States.StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.GamePlay.MainMenu
{
    public class MainMenuHud : MonoBehaviour
    {
        [SerializeField] private Button _gameStartButton;
        [SerializeField] private Button _progressResetButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private TMP_Text _highScoreText;
        private IGameStateMachine _gameStateMachine;
        private IProgressProvider _progressProvider;
        private const string GameplaySceneName = "Gameplay";

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine, IProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
            _gameStateMachine = gameStateMachine;
        }

        private void Start()
        {
            InitButtons();
        }

   

        private void InitButtons()
        {
            _gameStartButton.onClick.AddListener(EnterGameplayScene);
            _progressResetButton.onClick.AddListener(ResetProgress);
            _exitButton.onClick.AddListener(Exit);
        }

        private void EnterGameplayScene()
        {
            _gameStateMachine.Enter<LoadingGameplayState, string>(GameplaySceneName);
        }

        private void ResetProgress()
        {
            _progressProvider.DeleteProgress();
            _gameStateMachine.Enter<InitializeProgressState>();
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}