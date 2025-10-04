using Assets.Code.Common.Utils.ActionList.Editor;
using Assets.Code.GamePlay.DataDriven.Effects;
using UnityEditor;

namespace Assets.Code.GamePlay.DataDriven.Editor
{
    [CustomPropertyDrawer(typeof(Effect))]

    public class EffectDrawer: ActionListAttributeDrawer<Effect>
    {
        
    }
}