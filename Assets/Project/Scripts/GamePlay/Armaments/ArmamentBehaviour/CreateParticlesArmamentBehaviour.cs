using Assets.Code.GamePlay.Armaments.ArmamentBehaviour.Abstract;
using Assets.Code.GamePlay.Armaments.Projectiles.Factories;
using UnityEngine;

namespace Assets.Code.GamePlay.Armaments.ArmamentBehaviour
{
    public class CreateParticlesArmamentBehaviour: IArmamentBehaviour,IStartableBehaviour,IOnDestroyableBehaviour
    {
        private readonly ParticleSystem[] _startParticles;
        private readonly ParticleSystem[] _destroyParticles;
        private IArmamentsFactory _armamentsFactory;
        private Armament _armament;

        public CreateParticlesArmamentBehaviour(IArmamentsFactory armamentsFactory,ParticleSystem[] startParticles, ParticleSystem[] destroyParticles)
        {
            _armamentsFactory = armamentsFactory;
            _startParticles = startParticles;
            _destroyParticles = destroyParticles;
      
        }
        public void InitArmament(Armament armament)
        {
            _armament = armament;
        }

        public void Start()
        {
            _armamentsFactory.CreateArmamentParticles(_armament, _startParticles);
        }

        public void OnDestroy()
        {
            _armamentsFactory.CreateArmamentParticles(_armament, _destroyParticles);

        }
    }
}