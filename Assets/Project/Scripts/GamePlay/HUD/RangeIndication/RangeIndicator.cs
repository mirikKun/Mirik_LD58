using Assets.Code.GamePlay.Physic.Raycast;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.GamePlay.HUD.HudEffects
{
    public class RangeIndicator:MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Color _inRangeColor = Color.white;
        [SerializeField] private Color _outOfRangeColor = new Color(1,1,1,0.3f);
        private RangeIndicationType _type;
        private RaycastSensor _raycastSensor;
        public RangeIndicationType Type=> _type;

        public void Init(RangeIndicationData indicationData, RaycastSensor raycastSensor)
        {
            _raycastSensor = raycastSensor;
            _icon.sprite=indicationData.Icon;
            _icon.rectTransform.sizeDelta = indicationData.Size;
            _type = indicationData.Type;
        }

        public void Tick()
        {
            _icon.color=_raycastSensor.CastAndCheck()?_inRangeColor:_outOfRangeColor;
        }
    }
}