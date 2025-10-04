using System;
using Assets.Code.GamePlay.Player.Inventory.Enums;
using Assets.Code.GamePlay.Player.Inventory.General;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Code.GamePlay.Player.Inventory.UI.Core
{
    public abstract class InventorySlotUI : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] protected Image _itemImage;
        [SerializeField] protected Image _activeFrameImage;
        [SerializeField] protected Image _availableFrameImage;
        [SerializeField] protected bool _active;


        protected SelectionType _selectionType;


        public bool IsActive => _active;
        public IInventoryItem Item { protected set; get; }
        public abstract SlotType SlotType { get; }
        
        public event Action<InventorySlotUI> OnSlotSelected;

        public abstract bool SameType(InventorySlotUI otherSlot);

        public virtual void SetSlot(IInventoryItem item)
        {
            if (item == null)
            {
                ClearSlot();
                return;
            }
            
            Item = item;
            _itemImage.sprite = item.Icon;
            _itemImage.color = Color.white;
        }

        public virtual void ClearSlot()
        {
            Item = null;
            _itemImage.sprite = null;
            _itemImage.color = new Color(1,1,1,0);
        }

        public virtual void HighlightActiveSlot()
        {
            _activeFrameImage.gameObject.SetActive(true);
        }
        public virtual void HighlightAvailableSlot()
        {
            _availableFrameImage.gameObject.SetActive(true);
        }

        public virtual void UnhighlightSlot()
        {
            _activeFrameImage.gameObject.SetActive(false);
            _availableFrameImage.gameObject.SetActive(false);


        }
        public virtual void  OnPointerDown(PointerEventData eventData)
        {
            OnSlotSelected?.Invoke(this);
        }
    }
}