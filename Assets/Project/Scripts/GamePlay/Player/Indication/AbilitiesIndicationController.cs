using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;

namespace Project.Scripts.GamePlay.Player.Indication
{
    public class AbilitiesIndicationController:EntityComponent
    {
        private RangeIndication _rangeIndication;
        public RangeIndication RangeIndication=> _rangeIndication;
        
        private void Awake()
        {
            _rangeIndication = new RangeIndication();
        }
    }
}