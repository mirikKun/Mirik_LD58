using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Project.Scripts.Sounds
{
    public class SoundsSystem : ISoundsSystem
    {
        public void PlayOneShot(EventReference sound, Vector3 position=default)
        {
            if (!sound.IsNull)
            {
                RuntimeManager.PlayOneShot(sound, position);
            }
        }
        public EventInstance CreateInstance(EventReference sound)
        {
         
                EventInstance eventInstance = RuntimeManager.CreateInstance(sound);
                
                return eventInstance;
                   
        }
    }
}