using System;
using UnityEngine;

namespace Project.Scripts.GamePlay.VFX
{
    public class ScaleOnEnable:MonoBehaviour
    {
        [SerializeField] private float _scaleSpeed=15;
        private float _currentScale;
        
        private void Update()
        {
            transform.localScale=Vector3.one*Mathf.Lerp(_currentScale,1,Time.deltaTime*_scaleSpeed);
            _currentScale = transform.localScale.x;
        }

        private void OnEnable()
        {
            _currentScale = 0;
        }
    }
}