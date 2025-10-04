using System;
using Assets.Code.GamePlay.Common.Entity;
using Assets.Code.GamePlay.Physic.Raycast;
using Assets.Code.GamePlay.PickUp;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.Player.Controller
{
    public class PlayerPickUpper:EntityComponent
    {
        [SerializeField] private Transform _raycastOrigin;
        [SerializeField] private float _checkDistance = 2f;
        [SerializeField] private LayerMask _pickableLayer;
        
        private RaycastSensor _raycastSensor;
        private IPickUp _lastPickUp;
        private IInputReader _inputReader;
        private bool _isInteractPressed;

        [Inject]
        private void Construct(IInputReader inputReader )
        {
            _inputReader = inputReader;
        }
        public void Start()
        {
            _raycastSensor = new RaycastSensor(_raycastOrigin);
            _raycastSensor.CastLength = (_checkDistance);
            _raycastSensor.SetCastDirection(RaycastSensor.CastDirection.Forward);
            _inputReader.Interact+= OnInteractInput;
        }

        private void OnInteractInput(bool pressed)
        {
            _isInteractPressed= pressed;
        }

        private void OnDestroy()
        {
            _inputReader.Interact-= OnInteractInput;
        }

        public void Tick()
        {
            _raycastSensor.Cast();
            if (_raycastSensor.HasDetectedHit()&&_raycastSensor.GetCollider().TryGetComponent<IPickUp>(out var pickUp))
            {
                
                if (_lastPickUp != pickUp)
                {
                    UnHighlightLast();
                    _lastPickUp = pickUp;
                    pickUp.HighLight();
                }
                if (_isInteractPressed)
                {
                    pickUp.PickUp();
                    UnHighlightLast();

                }
            }
            else if (_lastPickUp!=null)
            {
                UnHighlightLast();
            }
        }

        private void UnHighlightLast()
        {
            _lastPickUp?.UnHighLight();
            _lastPickUp = null;
        }
    }
}