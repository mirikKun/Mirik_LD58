using Assets.Code.GamePlay.Armaments.ArmamentBehaviour.Abstract;
using Code.Gameplay.Common.Time;

namespace Assets.Code.GamePlay.Armaments.ArmamentBehaviour
{
    public class LifetimeArmamentBehaviour:IArmamentBehaviour,IUpdateableArmament
    {
        private float _defaultMaxLifeTime = 15f; 
        private Armament _armament;

        private float _maxLifeTime;
        private float _currentLifeTime;
  
        public LifetimeArmamentBehaviour( float maxLifeTime)
        {
            _maxLifeTime = maxLifeTime;
        }  
        public LifetimeArmamentBehaviour( )
        {
            _maxLifeTime = _defaultMaxLifeTime;
        }
        public void InitArmament(Armament armament)
        {
            _armament = armament;

        }
        public void Tick(float deltaTime)
        {
            ChangeLifetime(deltaTime);
        }

        private void ChangeLifetime(float deltaTime)
        {
            _currentLifeTime += deltaTime;
            if (_currentLifeTime > _maxLifeTime)
            {
                _armament.Destroy();
            }
        }
    }
}