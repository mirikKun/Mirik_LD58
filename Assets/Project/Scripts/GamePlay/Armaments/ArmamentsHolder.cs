using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.GamePlay.Armaments.Configs;
using Project.Scripts.GamePlay.Armaments.Enums;
using Project.Scripts.GamePlay.Armaments.Factories;
using UnityEngine;
using Zenject;

namespace Project.Scripts.GamePlay.Armaments
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

        public Armament CreateArmament(ArmamentConfig config,bool hitCaster)
        {
            Transform placement = GetArmamentPlacement(config);
            Transform parent = config.CasterIsParent ? placement : null;
            Quaternion rotation = placement.rotation;
            Vector3 placementPosition = placement.position;

            var armament = CreateArmament(config, placementPosition, rotation, parent,hitCaster);
            return armament;
        }

        public Armament CreateArmament(ArmamentConfig config, Vector3 at, Quaternion rotation, Transform parent,bool hitCaster)
        {
            Armament armament = _armamentsFactory.CreateArmament(config, at, rotation, parent);
            armament.Destroyed += OnArmamentDestroyed;
            armament.Init(Entity, config);
            if(!hitCaster)
            {
                armament.gameObject.layer = _armamentLayer;
            }           
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
                throw new Exception($"Attempted to remove an armament {armament} that does not exist in the holder.");
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
            ArmamentPlacementType configArmamentPlacementType = config.ArmamentPlacementType;
            return GetArmamentPlacement(configArmamentPlacementType);
        }

        public Transform GetArmamentPlacement(ArmamentPlacementType type)
        {
            foreach (var placement in _armamentPlacements)
            {
                if (placement.PlacementType == type)
                {
                    return placement.Parent;
                }
            }

            throw new Exception($"No armament placement found for type: {type}");
        }

        public ArmamentIndicator CreateIndicator(IndicatorType indicatorType,Vector3 at, Quaternion rotation)
        {
            ArmamentIndicator currentIndicator = _armamentsFactory.CreateIndicator(indicatorType, at, rotation);
            if (currentIndicator != null)
            {
                currentIndicator.Show();
            }

            return currentIndicator;
        }
    }

    [Serializable]
    public class ArmamentPlacement
    {
        [field: SerializeField] public ArmamentPlacementType PlacementType { get; private set; }
        [field: SerializeField] public Transform Parent { get; private set; }
    }
}