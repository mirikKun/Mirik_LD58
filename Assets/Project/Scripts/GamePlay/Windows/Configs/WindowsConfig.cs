using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.GamePlay.Windows.Configs
{
    [CreateAssetMenu(fileName = "WindowsConfig", menuName = "Configs/Windows Config")]
    public class WindowsConfig : ScriptableObject
    {
        public List<WindowConfig> WindowConfigs;
    }
}