using Assets.Code.Common.Utils.ActionList.Editor;
using Assets.Code.GamePlay.Enemies.EnemyController.Combat.Attributes;
using UnityEditor;

namespace Assets.Code.GamePlay.Enemies.EnemyController.Combat.Editor
{
    [CustomPropertyDrawer(typeof(AttackListAttribute))]
    public class AttackListAttributeDrawer : ActionListAttributeDrawer<IEnemyAttack>
    {
        
    }
}