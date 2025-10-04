using Assets.Code.Common.Utils.ActionList;
using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.Combat
{ 
    public interface IEnemyAttack:IActionElement
    {
        public void Init(ActorEntity casterEntity);

        public string AttackId { get; }
        public void Attack(Vector3 target,Vector3 direction);
        public void StopAttack(){}
    }
}