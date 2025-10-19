using Project.Scripts.GamePlay.Player.Abilities.Configs;
using Project.Scripts.GamePlay.Player.Abilities.Enums;
using UnityEngine.Events;

namespace Project.Scripts.GamePlay.Player.Abilities.General
{
    public class AbilityInstance
    {
        public readonly AbilitySlotKey SlotKey;

        public readonly BaseAbilityConfig AbilityConfig;

        public event UnityAction<bool> OnAbilityInput;

        public AbilityInstance(AbilitySlotKey slotKey, BaseAbilityConfig abilityConfig)
        {
            SlotKey = slotKey;
            AbilityConfig = abilityConfig;
        }

        public void OnKeyInput(bool isPressed)
        {
            OnAbilityInput?.Invoke(isPressed);
        }
        public void Clear()
        {
            OnAbilityInput = null;
        }
        
        
    }
}