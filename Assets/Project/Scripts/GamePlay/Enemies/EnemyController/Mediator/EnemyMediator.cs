using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.Mediator
{
    public class EnemyMediator:AgentMediator<EnemyEntity>
    {

        protected override bool MediatorConditionMet(EnemyEntity target)
        {
            return true;
        }
        protected override void OnRegistered(EnemyEntity enemy)
        {
            Debug.Log($"{enemy.name} registered");
            Broadcast(enemy,new MessagePayload.Builder(enemy).WithContent("Registered").Build());
        }
        protected override void OnDeregistered(EnemyEntity enemy)
        {
            Debug.Log($"{enemy.name} deregistered");
            Broadcast(enemy,new MessagePayload.Builder(enemy).WithContent("Deregistered").Build());
        }
    }
}