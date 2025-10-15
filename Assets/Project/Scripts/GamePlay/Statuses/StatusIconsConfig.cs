using UnityEngine;

namespace Project.Scripts.GamePlay.Statuses
{
    [CreateAssetMenu(fileName = "StatusIconsConfig", menuName = "Configs/Icons/StatusIconsConfig", order = 0)]
    public class StatusIconsConfig:ScriptableObject
    {
        [SerializeField] private StatusIcon[] _statusIcons;
        public Sprite GetIcon(StatusType statusType)
        {
            foreach (var statusIcon in _statusIcons)
            {
                if (statusIcon.Status == statusType)
                {
                    return statusIcon.Icon;
                }
            }
            return null;
        }
    }
    [System.Serializable]
    public class StatusIcon
    {
        public StatusType Status;
        public Sprite Icon;
    }
}