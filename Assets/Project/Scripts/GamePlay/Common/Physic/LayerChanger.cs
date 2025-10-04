using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;

namespace Assets.Code.GamePlay.Common.Physic
{
    public class LayerChanger:EntityComponent
    {
        [SerializeField] private GameObject _objectToChangeLayer;
        [SerializeField] private int _defaultLayer;
        [SerializeField] private int _intangibleLayer;
        
        [ContextMenu("Change Layer To Default")]
        public void ChangeLayerToDefault()
        {
            _objectToChangeLayer.layer = _defaultLayer;
        }

        [ContextMenu("Change Layer To Invincible")]
        public void ChangeLayerToIntangible()
        {
            _objectToChangeLayer.layer = _intangibleLayer;
        }
    }
}