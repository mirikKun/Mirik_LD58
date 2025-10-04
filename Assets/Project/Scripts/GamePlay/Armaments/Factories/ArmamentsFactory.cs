using Assets.Code.GamePlay.Armaments.ArmamentBehaviour;
using Assets.Code.GamePlay.Armaments.Projectiles.Enums;
using Code.Gameplay.StaticData;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.Armaments.Projectiles.Factories
{
    public class ArmamentsFactory:IArmamentsFactory
    {
        private IInstantiator _instantiator;
        private IStaticDataService _staticDataService;
        private Transform _parent;

        [Inject]
        private void Construct(IInstantiator instantiator,IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
            _instantiator = instantiator;
        }

        public void SetupArmamentsParent(Transform parent)
        {
            _parent = parent;
        }

        public ArmamentConfig GetArmamentConfig(ArmamentType armamentType)
        {
            return _staticDataService.GetProjectilesConfig().GetProjectileData(armamentType);
        }

        public Armament CreateArmament(ArmamentType armamentType,Vector3 at,Quaternion rotation,Transform parent=null)
        {
            ArmamentConfig armamentData = GetArmamentConfig(armamentType);
            return CreateArmament(armamentData, at, rotation,parent);
        }

        public Armament CreateArmament(ArmamentConfig armamentData,Vector3 at,Quaternion rotation,Transform parent=null)
        {
            Transform newParent =parent != null ? parent : _parent;
            var armament = _instantiator.InstantiatePrefabForComponent<Armament>(armamentData.ArmamentPrefab,newParent);
            
            armament.transform.position = at;
            armament.transform.rotation = rotation;


            armament.With(new CreateParticlesArmamentBehaviour(this, armamentData.ParticlesToSpawnOnStart,
                armamentData.ParticlesToSpawnOnDestroy));

            armament.Destroyed += OnArmamentDestroyed;
            return armament;
        }
        

        private void OnArmamentDestroyed(Armament armament)
        {
            armament.Destroyed -= OnArmamentDestroyed;
            

        }

        public void CreateArmamentParticles(Armament armament,ParticleSystem[] particleSystems)
        {
            
            foreach (var particleSystem in particleSystems)
            {
                ParticleSystem newParticle=Object.Instantiate(particleSystem,armament.transform.position,armament.transform.rotation,_parent);
                newParticle.Play();
                Object.Destroy(newParticle.gameObject,newParticle.main.duration);
            }
        }
   
    }
}