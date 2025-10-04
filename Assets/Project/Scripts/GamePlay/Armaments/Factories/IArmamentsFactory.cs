using Assets.Code.GamePlay.Armaments.Projectiles.Enums;
using UnityEngine;

namespace Assets.Code.GamePlay.Armaments.Projectiles.Factories
{
    public interface IArmamentsFactory
    {
        void SetupArmamentsParent(Transform parent);
        Armament CreateArmament(ArmamentType armamentType,Vector3 at,Quaternion rotation,Transform parent=null);
        Armament CreateArmament(ArmamentConfig armamentData,Vector3 at,Quaternion rotation,Transform parent=null);
        void CreateArmamentParticles(Armament armament,ParticleSystem[] particleSystems);
        ArmamentConfig GetArmamentConfig(ArmamentType armamentType);
    }
}