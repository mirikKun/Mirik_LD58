using System.Collections.Generic;
using Assets.Code.GamePlay.Common.Entity;

namespace Assets.Code.GamePlay.DataDriven.Statuses
{
    public class StatusesContainer:EntityComponent
    {
        private List<BaseStatus> _statuses= new List<BaseStatus>();
        public void AddStatus(StatusConfig status)
        {
            //_statuses
        }
    }
}