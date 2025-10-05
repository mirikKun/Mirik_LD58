using System;
using Project.Scripts.GamePlay.Player.StealSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GamePlay.HUD
{
    public class ManaUI:MonoBehaviour
    {
        [SerializeField] private StealingController _stealingController;
        [SerializeField] private Image _fillImage;

        private void Start()
        {
            _stealingController.ManaChanged+=OnManaChanged;
            OnManaChanged(1);
        }
        private void OnDestroy()
        {
            _stealingController.ManaChanged-=OnManaChanged;
        }
        private void OnManaChanged(float manaNormalized)
        {
            _fillImage.fillAmount = manaNormalized;
        }
    }
    
}