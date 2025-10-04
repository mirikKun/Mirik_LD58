using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.GamePlay.Common.Entity
{
    public abstract class BaseEntity:MonoBehaviour
    {
        [SerializeField] protected List<Component> _componentsList;
        public ComponentsRegistry Components { get; protected set; }
        public T Get<T>() where T : class
        {
            return Components.Get<T>();
        }
        public bool TryGet<T>(out T component) where T : class
        {
            return Components.TryGet(out component);
        }

        private void Awake()
        {
            InitComponentsRegistry();
        }

        protected virtual void InitComponentsRegistry()
        {
            Components= new ComponentsRegistry(_componentsList);
        }
        [ContextMenu("Get Components")]
        private void GetComponents()
        {
            _componentsList = new List<Component>(GetComponentsInChildren<EntityComponent>());
        }
    }
}