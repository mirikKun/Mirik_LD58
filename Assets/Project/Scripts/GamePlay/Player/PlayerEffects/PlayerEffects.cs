using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;

namespace Assets.Code.GamePlay.Player.PlayerEffects
{
    public class PlayerEffects:EntityComponent
    {
        [SerializeField] private HookEffects _hookEffects;
        [SerializeField] private TrajectoryEffects _trajectoryEffects;
        [SerializeField] private CameraMovingEffects _cameraMovingEffects;
        [SerializeField] private TimeSlowEffect _timeSlowEffect;
        public HookEffects HookEffects => _hookEffects;
        public TrajectoryEffects TrajectoryEffects => _trajectoryEffects;
        public CameraMovingEffects CameraMovingEffects => _cameraMovingEffects;
        public TimeSlowEffect TimeSlowEffect => _timeSlowEffect;
    }
}