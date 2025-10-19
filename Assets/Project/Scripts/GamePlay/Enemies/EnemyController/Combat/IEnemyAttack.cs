using Assets.Code.GamePlay.Common.Entity;
using Project.Scripts.Utils.ActionList;
using UnityEngine;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.Combat
{ 
    public interface IEnemyAttack:IActionElement
    {
        public void Init(ActorEntity casterEntity);

        public string AttackId { get; }
        public void Attack(Vector3 target,Vector3 direction);
        public void StopAttack(){}
    }
}