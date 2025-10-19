using Assets.Code.GamePlay.DataDriven.Effects;
using Project.Scripts.Utils.ActionList.Editor;
using UnityEditor;

namespace Assets.Code.GamePlay.DataDriven.Editor
{
    [CustomPropertyDrawer(typeof(Effect))]

    public class EffectDrawer: ActionListAttributeDrawer<Effect>
    {
        
    }
}