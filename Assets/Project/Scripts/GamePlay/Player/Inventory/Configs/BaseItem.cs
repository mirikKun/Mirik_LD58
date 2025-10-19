using Project.Scripts.GamePlay.Player.Inventory.General;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.Inventory.Configs
{
    public abstract class BaseItem:ScriptableObject, IInventoryItem
    {
        [SerializeField] private string itemID;
        [SerializeField] private string itemName;
        [SerializeField] private string itemDescription;
        [SerializeField] private Sprite itemIcon;
        [SerializeField] private int maxStackSize = 1;
        [SerializeField] private bool isStackable = false;

        public string ID => itemID;
        public string Name => itemName;
        public string Description => itemDescription;
        public Sprite Icon => itemIcon;
        public int MaxStackSize => maxStackSize;
        public bool IsStackable => isStackable;
    }
}