using System;
using Project.Scripts.GamePlay.Player.PlayerResources;
using Project.Scripts.GamePlay.Player.StealSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.GamePlay.HUD
{
    public class ManaUI:MonoBehaviour
    {
        [SerializeField] private PlayerManaController _playerManaController;
        [SerializeField] private Image _fillImage;

        private void Start()
        {
            _playerManaController.ManaChanged+=OnManaChanged;
            OnManaChanged(1);
        }
        private void OnDestroy()
        {
            _playerManaController.ManaChanged-=OnManaChanged;
        }
        private void OnManaChanged(float manaNormalized)
        {
            _fillImage.fillAmount = manaNormalized;
        }
    }
    
}