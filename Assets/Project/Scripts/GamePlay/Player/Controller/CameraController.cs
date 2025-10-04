using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.Player.Controller {
    public class CameraController : MonoBehaviour {
        [Range(0f, 90f)] public float _upperVerticalLimit =75f;
        [Range(0f, 90f)] public float _lowerVerticalLimit = 76f;

        [SerializeField] private float _cameraSpeed = 15f;

        [SerializeField] private Transform _horizontalPivot;
        [SerializeField] private Transform _verticalPivot;

        public Vector3 GetUpDirection() => _verticalPivot.up;
        public Vector3 GetFacingDirection () => _horizontalPivot.forward;

        public Transform CameraTrX=> _horizontalPivot;
        public Transform CameraTrY=> _verticalPivot;

        private float _currentXAngle;
        private float _currentYAngle;
        private IInputReader _input;


        [Inject]
        private void Construct(IInputReader input)
        {
            _input = input;
        }
        public void Awake() {
            
            _currentXAngle = _verticalPivot.localRotation.eulerAngles.x;
            _currentYAngle = _horizontalPivot.localRotation.eulerAngles.y;
        }

        public void TickLateUpdate(float deltaTime) 
        {
            RotateCamera(deltaTime,_input.LookDirection.x, -_input.LookDirection.y);
        }


        private void RotateCamera(float deltaTime,float horizontalInput, float verticalInput) 
        {
            _currentYAngle += horizontalInput * _cameraSpeed * deltaTime;
            
            _currentXAngle += verticalInput * _cameraSpeed * deltaTime;
            _currentXAngle = Mathf.Clamp(_currentXAngle, -_upperVerticalLimit, _lowerVerticalLimit);
            
            _horizontalPivot.localRotation = Quaternion.Euler(0, _currentYAngle, 0);
            _verticalPivot.localRotation = Quaternion.Euler(_currentXAngle, 0, 0);
        }

    
    }
}