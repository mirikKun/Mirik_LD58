using UnityEngine;

namespace Project.Scripts.GamePlay.Stats.Configs
{
    [CreateAssetMenu(fileName = "StatIconsConfig", menuName = "Configs/Icons/StatIconsConfig", order = 0)]
    public class StatIconsConfig:ScriptableObject
    {
        [SerializeField] private StatIcon[] _statIcons ;
        public Sprite GetIcon(StatType statType)
        {
            foreach (var statIcon in _statIcons)
            {
                if (statIcon.StatType == statType)
                    return statIcon.Icon;
            }
            return null;
        }
    }
    [System.Serializable]
    public class StatIcon
    {
        public StatType StatType;
        public Sprite Icon;
    }
}