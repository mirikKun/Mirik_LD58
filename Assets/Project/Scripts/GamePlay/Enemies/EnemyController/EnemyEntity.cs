using System;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.Mediator;
using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController
{
    [SelectionBase]
    public class EnemyEntity:ActorEntity,IVisitable
    {
        private EnemyMediator _enemyMediator;

        public void SetupEnemy(EnemyMediator enemyMediator)
        {
            _enemyMediator = enemyMediator;
            _enemyMediator.Register(this);
        }

        public void Accept(IVisitor message) => message.Visit(this);
        public void Send(IVisitor message) => _enemyMediator.Broadcast(this,message);
        public void Send(IVisitor message,Func<EnemyEntity,bool> predicate) => _enemyMediator.Broadcast(this,message,predicate);


    }
}