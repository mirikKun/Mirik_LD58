using UnityEngine;

namespace Assets.Code.GamePlay.Common.Entity
{
    public abstract class EntityComponent:MonoBehaviour
    {
        protected ActorEntity Entity;

        public virtual void InitEntity(ActorEntity entity)
        {
            Entity= entity;
        }
    }
}