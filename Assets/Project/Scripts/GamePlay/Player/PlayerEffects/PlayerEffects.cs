using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.PlayerEffects
{
    public class PlayerEffects : EntityComponent
    {
        [SerializeField] private HookEffects _hookEffects;
        [SerializeField] private TrajectoryEffects _trajectoryEffects;
        [SerializeField] private CameraMovingEffects _cameraMovingEffects;
        [SerializeField] private TimeSlowEffect _timeSlowEffect;
        [SerializeField] private BookEffects _bookEffects;


        public HookEffects HookEffects => _hookEffects;
        public TrajectoryEffects TrajectoryEffects => _trajectoryEffects;
        public CameraMovingEffects CameraMovingEffects => _cameraMovingEffects;
        public TimeSlowEffect TimeSlowEffect => _timeSlowEffect;
        public BookEffects BookEffects => _bookEffects;

        public void  Start()
        {
            // _hookEffects.InitEffect(Entity);
            // _trajectoryEffects.InitEffect(Entity);
            _cameraMovingEffects.InitEffect(Entity);
            // _timeSlowEffect.InitEffect(Entity);
             _bookEffects.InitEffect(Entity);
        }

        public void Tick(float deltaTime)
        {
            CameraMovingEffects.Tick(deltaTime);
            BookEffects.Tick(deltaTime);
        }
    }
}