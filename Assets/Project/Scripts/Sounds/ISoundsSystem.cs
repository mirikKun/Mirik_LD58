using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Project.Scripts.Sounds
{
    public  interface ISoundsSystem
    {
        public void PlayOneShot(EventReference sound,Vector3 position=default);
        EventInstance CreateInstance(EventReference sound);
    }
}