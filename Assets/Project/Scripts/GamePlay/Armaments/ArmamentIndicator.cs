using UnityEngine;

namespace Project.Scripts.GamePlay.Armaments
{
    public class ArmamentIndicator : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private float _fadeSpeed = 5f;
        [SerializeField] private Color _indicatorColor = new Color(1f, 1f, 1f, 0.5f);
        
        private Material _material;
        private bool _isShown;
        private float _currentAlpha;

        private void Awake()
        {
            if (_renderer != null)
            {
                _material = _renderer.material;
            }
        }

        public void Show()
        {
            if (!_isShown)
            {
                _isShown = true;
                gameObject.SetActive(true);
            }
        }

        public void Hide()
        {
            if (_isShown)
            {
                _isShown = false;
                gameObject.SetActive(false);
            }
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        private void Update()
        {
            if (_material != null)
            {
                float targetAlpha = _isShown ? _indicatorColor.a : 0f;
                _currentAlpha = Mathf.Lerp(_currentAlpha, targetAlpha, Time.deltaTime * _fadeSpeed);
                
                Color color = _indicatorColor;
                color.a = _currentAlpha;
                _material.color = color;
            }
        }

        private void OnDestroy()
        {
            if (_material != null)
            {
                Destroy(_material);
            }
        }
    }
}

