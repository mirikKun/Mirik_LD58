using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Enemies.EnemyController.Combat;
using Assets.Code.GamePlay.Enemies.EnemyController.Combat.Attributes;
using Assets.Code.GamePlay.Enemies.EnemyController.Enum;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.Enemies.EnemyController
{
    public class EnemyCombat:EntityComponent
    {
        [SerializeField] private CombatLogicType _combatLogicType;
        [field:SerializeField] public EnemyCombatData CombatData { get; private set; }=new();

        [field:SerializeField] public List<EnemyPreAttackMark> PreAttackMarks { get; private set; } = new(); 
        
        [SerializeReference] [AttackList] private List<IEnemyAttack> _attacksList = new();
        private DiContainer _container;
        public CombatLogicType CombatLogicType => _combatLogicType;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        public void Init()
        { 
            _attacksList.ForEach(attack => attack.Init(Entity));
            _attacksList.ForEach(attack => _container.Inject(attack));
        }

        public void ShowPreAttackMark(string eventName)
        {
            foreach (var mark in PreAttackMarks)
            {
                if(mark.MarkId==eventName)
                {
                    mark.Execute();
                }
            }
        }
    
        
        public void Attack(string eventName, Vector3 target=default,Vector3 direction= default)
        {
            foreach (var attack in _attacksList)
            {
                if(eventName==attack.AttackId)
                {
                    attack.Attack(target,direction);
                }
            }
        }
        public void StopAttack(string eventName)
        {
            foreach (var attack in _attacksList)
            {
                if(eventName==attack.AttackId)
                {
                    attack.StopAttack();
                }
            }
        }
    }
}