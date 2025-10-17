using FMODUnity;
using UnityEngine;

namespace Project.Scripts.Sounds
{
    public class SoundsSystem : ISoundsSystem
    {
        public void PlayOneShot(EventReference sound, Vector3 position)
        {
            RuntimeManager.PlayOneShot(sound, position);
        }
    }
}