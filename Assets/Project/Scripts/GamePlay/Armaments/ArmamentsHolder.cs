using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Armaments.Projectiles.Enums;
using Assets.Code.GamePlay.Armaments.Projectiles.Factories;
using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.Armaments.Projectiles
{
    public class ArmamentsHolder : EntityComponent
    {
        [SerializeField] private int _armamentLayer;
        [SerializeField] private ArmamentPlacement[] _armamentPlacements;
        [SerializeField] private List<Armament> _armaments;
        private IArmamentsFactory _armamentsFactory;

        [Inject]
        private void Construct(IArmamentsFactory armamentsFactory)
        {
            _armamentsFactory = armamentsFactory;
        }

        public Armament CreateArmament(ArmamentConfig config)
        {
            Transform placement = GetArmamentPlacement(config);
            Transform parent = config.CasterIsParent ? placement : null;
            Quaternion rotation =  placement.rotation;
            Armament armament = _armamentsFactory.CreateArmament(config, placement.position, rotation, parent);
            armament.Destroyed += OnArmamentDestroyed;
            armament.Init(Entity, config);
            //armament.gameObject.layer = _armamentLayer;
            _armaments.Add(armament); 
            return armament;
        }

        public void RemoveArmament(Armament armament)
        {
            if (_armaments.Contains(armament))
            {
                armament.Destroy();
            }
            else
            {
                throw new Exception("Attempted to remove an armament that does not exist in the holder.");
            }
        }

        private void OnArmamentDestroyed(Armament armament)
        {
            armament.Destroyed -= OnArmamentDestroyed;

            if (_armaments.Contains(armament))
            {
                _armaments.Remove(armament);
            }
        }

        public Transform GetArmamentPlacement(ArmamentConfig config)
        {
            foreach (var placement in _armamentPlacements)
            {
                if (placement.PlacementType == config.ArmamentPlacementType)
                {
                    return placement.Parent;
                }
            }

            throw new Exception($"No armament placement found for type: {config.ArmamentPlacementType}");
        }
    }

    [Serializable]
    public class ArmamentPlacement
    {
        [field: SerializeField] public ArmamentPlacementType PlacementType { get; private set; }
        [field: SerializeField] public Transform Parent { get; private set; }
    }
}