using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Code.GamePlay.Abilities.Enums;
using Assets.Code.GamePlay.Player.Inventory;
using Assets.Code.GamePlay.Player.Inventory.Enums;
using Assets.Code.GamePlay.Player.Inventory.General;
using Assets.Code.GamePlay.Player.Inventory.UI;
using Assets.Code.GamePlay.Player.Inventory.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Code.GamePlay.Inventory.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform _inventoryUIRoot;
        public Transform InventoryUIRoot => _inventoryUIRoot;
        [SerializeField] private List<ActiveAbilitySlotUI> _activeAbilitySlots = new List<ActiveAbilitySlotUI>();
        [SerializeField] private List<InactiveAbilitySlotUI> _inactiveAbilitySlots = new List<InactiveAbilitySlotUI>();

[Header("Description")]
[SerializeField] private GameObject _descriptionRoot;
[SerializeField] private Image _image;
[SerializeField] private TextMeshProUGUI _nameText;
[SerializeField] private TextMeshProUGUI _descriptionText;
        private IInventorySystem _inventorySystem;
        private InventorySlotUI _selectedSlot;
        private List<InventorySlotUI> _highlightedSlots = new List<InventorySlotUI>();

        private bool _created;

        [Inject]
        private void Construct(IInventorySystem inventorySystem)
        {
            _inventorySystem = inventorySystem;
        }

        private void Start()
        {
            SetupActiveInventory(_inventorySystem.ActiveAbilities );
            SetupInactiveInventory(_inventorySystem.InactiveAbilities);

            foreach (var activeAbilitySlot in _activeAbilitySlots)
            {
                activeAbilitySlot.OnSlotSelected += OnSlotSelected;
            }

            foreach (var inactiveAbilitySlot in _inactiveAbilitySlots)
            {
                inactiveAbilitySlot.OnSlotSelected += OnSlotSelected;
            }
            _created= true;

        }

        private void OnEnable()
        {
            if (_created)
            {
                SetupInactiveInventory(_inventorySystem.InactiveAbilities);

            }
        }

        private void OnDisable()
        {
            _inventorySystem.OnActiveInventoryChanged();
        }


        private void SetupActiveInventory(List<AbilitySlot> activeAbilities)
        {
            for (int i = 0; i < _activeAbilitySlots.Count; i++)
            {
                if (activeAbilities.Find(x => x.SlotKey == _activeAbilitySlots[i].AbilitySlotKey) is { } activeAbility&&
                   !_activeAbilitySlots.Any(x=>x.Item==activeAbility.EquippedAbility))
                {
                    _activeAbilitySlots[i].SetSlot(activeAbility.EquippedAbility);
                }
                else
                {
                    _activeAbilitySlots[i].ClearSlot();
                }
            }
            
        }
        private void SetupInactiveInventory(List<IAbilityItem> inactiveAbilities)
        {
         

            for (int i = 0; i < _inactiveAbilitySlots.Count; i++)
            {
                if (i < inactiveAbilities.Count)
                {
                    _inactiveAbilitySlots[i].SetSlot(inactiveAbilities[i]);
                }
                else
                {
                    _inactiveAbilitySlots[i].ClearSlot();
                }
            }
        }


        public void OnSlotSelected(InventorySlotUI slot)
        {
            if (SelectedSlotIsNullOrInappropriate(slot))
            {
                SetSlotAsActive(slot);
            }
            else
            {
                if (SameSlot(slot))
                {
                    ChangeSlotActivity(slot);
                }
                else if (SameSlotType(slot))
                {
                    SetSlotAsActive(slot);
                }
                else
                {
                    MoveToSlot(slot);
                }
            }
        }

        private void ChangeSlotActivity(InventorySlotUI slot)
        {
            foreach (var highlightedSlot in _highlightedSlots)
            {
                highlightedSlot.UnhighlightSlot();
            }

            if (slot is ActiveSlotUI)
            {
                InactiveAbilitySlotUI emptyInactiveSlot = _inactiveAbilitySlots.Find(s => s.Item == null);
                MoveToSlot(emptyInactiveSlot);
            }
            else if (slot is InactiveSlotUI)
            {
                ActiveAbilitySlotUI emptyInactiveSlot = _activeAbilitySlots.Find(s => s.Item == null);
                if (emptyInactiveSlot != null)
                {
                    MoveToSlot(emptyInactiveSlot);
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(slot), "Unknown slot type");
            }

            ClearSelectedSlot();
        }

        private bool SameSlot(InventorySlotUI slot)
        {
            return _selectedSlot != null && _selectedSlot == slot;
        }

        private bool SelectedSlotIsNullOrInappropriate(InventorySlotUI slot)
        {
            return _selectedSlot == null || _selectedSlot.Item == null || !slot.SameType(_selectedSlot);
        }


        private bool SameSlotType(InventorySlotUI slot)
        {
            // return slot is InactiveSlotUI && _selectedSlot is InactiveSlotUI||
            //        slot is ActiveSlotUI && _selectedSlot is ActiveSlotUI;
            return false;
        }

        private void MoveToSlot(InventorySlotUI slot)
        {
            switch (slot.SlotType)
            {
                case SlotType.Equipment:

                    break;
                case SlotType.Ability:
                    ChangeActiveAbility(slot, _selectedSlot);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            IInventoryItem previousItem = slot.Item;

            slot.SetSlot(_selectedSlot.Item);
            _selectedSlot.SetSlot(previousItem);

            ClearSelectedSlot();
        }

        private void ClearSelectedSlot()
        {
            _selectedSlot?.UnhighlightSlot();
            _selectedSlot = null;

            foreach (var highlightedSlot in _highlightedSlots)
            {
                highlightedSlot.UnhighlightSlot();
            }

            _highlightedSlots.Clear();
            HideDescription();
        }

        private void SetSlotAsActive(InventorySlotUI slot)
        {
            ClearSelectedSlot();

            slot.HighlightActiveSlot();
            ShowDescription(slot);
            _selectedSlot = slot;
            HighlightAvailableSlots(slot);
        }

        private void HighlightAvailableSlots(InventorySlotUI chosenSlot)
        {
            foreach (var activeAbilitySlot in _activeAbilitySlots)
            {
                if (chosenSlot.Item is IAbilityItem abilityItem &&
                    activeAbilitySlot.AbilitySlotType == abilityItem.SlotType && activeAbilitySlot != chosenSlot)
                {
                    activeAbilitySlot.HighlightAvailableSlot();
                    _highlightedSlots.Add(activeAbilitySlot);
                }
            }
        }
        private void ShowDescription(InventorySlotUI slot)
        {
            if (slot.Item is IAbilityItem abilityItem)
            {
                _descriptionRoot.SetActive(true);
                _image.sprite = abilityItem.Icon;
                _nameText.text = abilityItem.Name;
                _descriptionText.text = abilityItem.Description;
            }
            else
            {
                _descriptionRoot.SetActive(false);

            }
        }
        private void HideDescription()
        {
            _descriptionRoot.SetActive(false);
        }


        private void ChangeActiveAbility(InventorySlotUI oldAbilitySlotUI, InventorySlotUI newAbilitySlotUI)
        {
            if (newAbilitySlotUI is InactiveAbilitySlotUI && oldAbilitySlotUI is ActiveAbilitySlotUI)
            {
                SetNewActiveAbility((InactiveAbilitySlotUI)newAbilitySlotUI, (ActiveAbilitySlotUI)oldAbilitySlotUI);
            }
            else if (newAbilitySlotUI is ActiveAbilitySlotUI && oldAbilitySlotUI is InactiveAbilitySlotUI)
            {
                SetNewActiveAbility((InactiveAbilitySlotUI)oldAbilitySlotUI, (ActiveAbilitySlotUI)newAbilitySlotUI);
            }
            else if (newAbilitySlotUI is ActiveAbilitySlotUI && oldAbilitySlotUI is ActiveAbilitySlotUI)
            {
                SwapActiveAbility((ActiveAbilitySlotUI)newAbilitySlotUI, (ActiveAbilitySlotUI)oldAbilitySlotUI);
            }
        }

        private void SetNewActiveAbility(InactiveAbilitySlotUI inactiveAbilitySlotUI,
            ActiveAbilitySlotUI activeAbilitySlotUI)
        {
            IAbilityItem activeAbilityItem = activeAbilitySlotUI.Item as IAbilityItem;
            AbilitySlotKey abilitySlotKey = activeAbilitySlotUI.AbilitySlotKey;
            IAbilityItem inactiveAbilityItem = inactiveAbilitySlotUI.Item as IAbilityItem;


            AbilitySlot newAbilitySlot = new AbilitySlot(abilitySlotKey, inactiveAbilityItem);
            _inventorySystem.RemoveActiveAbility(activeAbilityItem);
            _inventorySystem.RemoveInactiveAbility(inactiveAbilityItem);

            _inventorySystem.SetActiveAbility(newAbilitySlot);
            _inventorySystem.SetInactiveAbility(activeAbilityItem);
        }


        private void SwapActiveAbility(ActiveAbilitySlotUI oldAbilitySlotUI, ActiveAbilitySlotUI newAbilitySlotUI)
        {
            IAbilityItem oldAbilityItem = oldAbilitySlotUI.Item as IAbilityItem;
            AbilitySlotKey oldAbilitySlotKey = oldAbilitySlotUI.AbilitySlotKey;
            IAbilityItem newAbilityItem = newAbilitySlotUI.Item as IAbilityItem;
            AbilitySlotKey newAbilitySlotKey = newAbilitySlotUI.AbilitySlotKey;


            AbilitySlot newAbilitySlot = new AbilitySlot(oldAbilitySlotKey, newAbilityItem);
            AbilitySlot oldAbilitySlot = new AbilitySlot(newAbilitySlotKey, oldAbilityItem);
            _inventorySystem.RemoveActiveAbility(oldAbilityItem);
            _inventorySystem.RemoveActiveAbility(newAbilityItem);

            _inventorySystem.SetActiveAbility(newAbilitySlot);
            _inventorySystem.SetActiveAbility(oldAbilitySlot);
        }
    }
}