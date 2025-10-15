using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Player.PlayerIndicators;
using Project.Scripts.GamePlay.Player.StealSystem;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerEffects
{
    public class PlayerEffects:EntityComponent
    {
        [SerializeField] private HookEffects _hookEffects;
        [SerializeField] private TrajectoryEffects _trajectoryEffects;
        [SerializeField] private CameraMovingEffects _cameraMovingEffects;
        [SerializeField] private TimeSlowEffect _timeSlowEffect;
        [SerializeField] private BookEffects _bookEffects;

        [SerializeField] private PlayerAbilitiesPanel _playerAbilitiesPanel;
        
        public HookEffects HookEffects => _hookEffects;
        public TrajectoryEffects TrajectoryEffects => _trajectoryEffects;
        public CameraMovingEffects CameraMovingEffects => _cameraMovingEffects;
        public TimeSlowEffect TimeSlowEffect => _timeSlowEffect;
        public BookEffects BookEffects => _bookEffects;
        public PlayerAbilitiesPanel PlayerAbilitiesPanel => _playerAbilitiesPanel;

        public void Tick(float deltaTime)
        {
            CameraMovingEffects.Tick(deltaTime);
            BookEffects.Tick(deltaTime);
        }
    }
}