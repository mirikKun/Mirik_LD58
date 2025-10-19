using System;
using FMODUnity;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Sounds
{
    public class MusicController:MonoBehaviour
    {
        [SerializeField] private EventReference _music;
        [SerializeField] private SoundSource _soundSource;
        private ISoundsSystem _soundsSystem;
        
        private void Start()
        {
            var music=_soundSource.CreateInstance(_music);
            music.start();
        }
    }
}