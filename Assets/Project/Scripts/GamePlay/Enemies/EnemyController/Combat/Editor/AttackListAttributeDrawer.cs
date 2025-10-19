using Project.Scripts.GamePlay.Enemies.EnemyController.Combat.Attributes;
using Project.Scripts.Utils.ActionList.Editor;
using UnityEditor;

namespace Project.Scripts.GamePlay.Enemies.EnemyController.Combat.Editor
{
    [CustomPropertyDrawer(typeof(AttackListAttribute))]
    public class AttackListAttributeDrawer : ActionListAttributeDrawer<IEnemyAttack>
    {
        
    }
}