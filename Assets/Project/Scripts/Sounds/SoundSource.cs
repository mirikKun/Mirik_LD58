using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Sounds
{
    public class SoundSource : EntityComponent
    {
        [SerializeField] private SoundPlacement[] _soundPlacements;
        private List<EventInstance> _eventInstances = new List<EventInstance>();
        private ISoundsSystem _soundsSystem;

        [Inject]
        private void Construct(ISoundsSystem soundsSystem)
        {
            _soundsSystem = soundsSystem;
        }

        public void PlaySound(EventReference sound, SoundPlacementType placementType = SoundPlacementType.None)
        {
            Vector3 position = GetPositionForSound(placementType);
            _soundsSystem.PlayOneShot(sound, position);
        }
        public EventInstance CreateInstance(EventReference sound, SoundPlacementType placementType = SoundPlacementType.None)
        {
            Vector3 position = GetPositionForSound(placementType);
            EventInstance eventInstance = _soundsSystem.CreateInstance(sound);
            _eventInstances.Add(eventInstance);
            return eventInstance;
        }



        private Vector3 GetPositionForSound(SoundPlacementType type)
        {
            foreach (var placement in _soundPlacements)
            {
                if (placement.Type == type && placement.Transform != null)
                {
                    return placement.Transform.position;
                }
            }

            return transform.position;
        }
    }

    public enum SoundPlacementType
    {
        None,
        Root,
        Center,
        Head,
        LeftHand,
        RightHand,
    }

    [System.Serializable]
    public class SoundPlacement
    {
        public SoundPlacementType Type;
        public Transform Transform;
    }
}