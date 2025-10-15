using System;
using Assets.Code.GamePlay.Physic.Raycast;
using Project.Scripts.GamePlay.HUD.HudEffects;

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