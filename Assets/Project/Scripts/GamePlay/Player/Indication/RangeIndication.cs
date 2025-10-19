using System;
using Project.Scripts.GamePlay.Common.Physic.Raycast;
using Project.Scripts.GamePlay.HUD.HudEffects;
using Project.Scripts.GamePlay.HUD.RangeIndication;

namespace Project.Scripts.GamePlay.Player.Indication
{
    public class RangeIndication
    {
        public event Action<RangeIndicationType,RaycastSensor> AbilityWithRangeEquipped;
        public event Action<RangeIndicationType> AbilityWithRangeUnequipped;
        
        public void EquipAbilityWithRange(RangeIndicationType type,RaycastSensor raycastSensor)
        {
            AbilityWithRangeEquipped?.Invoke(type,raycastSensor);
        }
        public void UnequipAbilityWithRange(RangeIndicationType type)
        {
            AbilityWithRangeUnequipped?.Invoke(type);
        }
    }
}