using FMODUnity;
using UnityEngine;

namespace Project.Scripts.Sounds
{
    public  interface ISoundsSystem
    {
        public void PlayOneShot(EventReference sound,Vector3 position);
    }
}