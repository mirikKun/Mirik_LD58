using Project.Scripts.GamePlay.Armaments.ArmamentBehaviour.Abstract;
using Project.Scripts.GamePlay.Armaments.Factories;
using UnityEngine;

namespace Project.Scripts.GamePlay.Armaments.ArmamentBehaviour
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

        public void StartBehaviour()
        {
            _armamentsFactory.CreateArmamentParticles(_armament, _startParticles);
        }

        public void OnDestroy()
        {
            _armamentsFactory.CreateArmamentParticles(_armament, _destroyParticles);

        }
    }
}