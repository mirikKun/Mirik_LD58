
using UnityEngine;

namespace Project.Scripts.Infrastructure.ServiceLocator
{
    public class BootstrapServiceInstaller: MonoBehaviour,IServiceInstaller
    {

       // [SerializeField] private SoundSystemAsset _globalSoundSystemAsset;
        public void Install()
        {
            // SoundSystem soundSystem = new SoundSystem();
            // ServiceLocator.Global.Register<ISoundSystem>(soundSystem);
            // soundSystem.LoadSoundAsset(_globalSoundSystemAsset);
            // ServiceLocator.Global.Register<ISoundSettingsService>(new SoundSettingsService(soundSystem));
        }
    }
}