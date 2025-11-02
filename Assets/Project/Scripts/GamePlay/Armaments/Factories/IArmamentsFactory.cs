using Project.Scripts.GamePlay.Armaments.Configs;
using Project.Scripts.GamePlay.Armaments.Enums;
using UnityEngine;

namespace Project.Scripts.GamePlay.Armaments.Factories
{
    public interface IArmamentsFactory
    {
        void SetupArmamentsParent(Transform parent);
        Armament CreateArmament(ArmamentType armamentType,Vector3 at,Quaternion rotation,Transform parent=null);
        Armament CreateArmament(ArmamentConfig armamentData,Vector3 at,Quaternion rotation,Transform parent=null);
        void CreateArmamentParticles(Armament armament,ParticleSystem[] particleSystems);
        ArmamentConfig GetArmamentConfig(ArmamentType armamentType);
        ArmamentIndicator CreateIndicator(IndicatorType indicatorType, Vector3 position, Quaternion rotation, Transform parent = null);
    }
}