using Unity.Cinemachine;
using UnityEngine;

namespace Code.Gameplay.Levels
{
    public interface ILevelDataProvider
    {
        Transform PlayerSpawnTransform { get; }
        Transform LevelGeneratorTransform { get; }
        CinemachineCamera MainCamera { get; }
        void SetStartPoint(Transform spawnTransform);
        void SetLevelGeneratorTransform(Transform levelGeneratorTransform);
        void SetCamera(CinemachineCamera mainCamera);
    }
}