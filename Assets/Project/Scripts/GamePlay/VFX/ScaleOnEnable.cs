using System;
using UnityEngine;

namespace Project.Scripts.GamePlay.VFX
{
    public class ScaleOnEnable:MonoBehaviour
    {
        [SerializeField] private float _scaleSpeed=15;
        [SerializeField] private Vector3 _targetScale=Vector3.one;
        [SerializeField] private Vector3 _startScale=Vector3.zero;
        private Vector3 _currentScale;
        
        private void Update()
        {
            transform.localScale=Vector3.Lerp(_currentScale,_targetScale,Time.deltaTime*_scaleSpeed);
            _currentScale = transform.localScale;
        }

        private void OnEnable()
        {
            _currentScale = _startScale;
        }
    }
}