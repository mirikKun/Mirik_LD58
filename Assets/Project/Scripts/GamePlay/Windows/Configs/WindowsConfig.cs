using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Windows.Configs
{
    [CreateAssetMenu(fileName = "WindowsConfig", menuName = "Configs/Windows Config")]
    public class WindowsConfig : ScriptableObject
    {
        public List<WindowConfig> WindowConfigs;
    }
}