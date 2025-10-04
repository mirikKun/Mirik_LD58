using Assets.Code.Common.Utils.ActionList.Editor;
using Assets.Code.GamePlay.DataDriven.Statuses;
using UnityEditor;

namespace Assets.Code.GamePlay.DataDriven.Editor
{
    [CustomPropertyDrawer(typeof(BaseStatus))]

    public class StatusDrawer: ActionListAttributeDrawer<BaseStatus>
    {
        
    }
}