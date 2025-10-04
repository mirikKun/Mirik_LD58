using System.Collections.Generic;
using Assets.Code.GamePlay.Armaments.Enums;
using Assets.Code.GamePlay.Armaments.Projectiles.Enums;
using Assets.Code.GamePlay.DataDriven.Effects;
using UnityEngine;

namespace Assets.Code.GamePlay.Armaments
{
    [CreateAssetMenu(fileName = "ArmamentConfig", menuName = "Configs/Armament/ArmamentConfig")]
    public class ArmamentConfig : ScriptableObject
    {
        [field:SerializeField] public ArmamentType Type { get; private set; }

        [field: SerializeField] public Armament ArmamentPrefab { get; private set; }
        [field: SerializeReference] public List<Effect> Effects { get; private set; }
        [field:SerializeField] public ParticleSystem[] ParticlesToSpawnOnStart { get; private set; } 
        [field:SerializeField] public ParticleSystem[] ParticlesToSpawnOnDestroy { get; private set; } 
        [field: SerializeField] public ArmamentHitType ArmamentHitType { get; private set; }
        [field: SerializeField] public ArmamentPlacementType ArmamentPlacementType { get; private set; }
        [field: SerializeField] public bool CasterIsParent { get; private set; } = true;


        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field:SerializeField] public ArmamentConfig ArmamentToSpawnOnDestroy { get; private set; }
    }
}