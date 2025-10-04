using Unity.Cinemachine;
using UnityEngine;

namespace Code.Gameplay.Levels
{
    public class LevelDataProvider : ILevelDataProvider
    {
        public Transform PlayerSpawnTransform { get; private set; }
        public Transform LevelGeneratorTransform { get; private set; }

        public CinemachineCamera MainCamera { get; private set; }


        public void SetStartPoint(Transform spawnTransform)
        {
            PlayerSpawnTransform = spawnTransform;
        }

        public void SetLevelGeneratorTransform(Transform levelGeneratorTransform)
        {
            LevelGeneratorTransform = levelGeneratorTransform;
        }

        public void SetCamera(CinemachineCamera mainCamera)
        {
            MainCamera = mainCamera;
        }
    }
}