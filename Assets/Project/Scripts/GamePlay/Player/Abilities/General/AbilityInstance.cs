using Assets.Code.GamePlay.Abilities.Enums;
using Assets.Code.GamePlay.Player.Abilities.Configs;
using Assets.Code.GamePlay.Player.PlayerStateMachine.StateConfigs;
using UnityEngine.Events;

namespace Assets.Code.GamePlay.Abilities.General
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