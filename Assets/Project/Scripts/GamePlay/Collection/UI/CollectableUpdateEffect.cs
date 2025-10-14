using DG.Tweening;
using Project.Scripts.Utils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.GamePlay.Collection.UI
{
    public class CollectableUpdateEffect:MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Graphic _graphic;
        [SerializeField] private float _distance = 1.3f;
        [SerializeField] private float _appearDuration = 1.4f;
        [SerializeField] private float _stayDuration = 1.4f;
        [SerializeField] private float _hideDuration = 1.4f;
        [SerializeField] private Ease _easeIn= Ease.InBack;
        [SerializeField] private Ease _easeOut= Ease.OutBack;

        private Vector3 _startPosition;
        private void Start()
        {
            _startPosition = _transform.position;
        }

        public void PlayEffect()
        {
            _transform.position = _startPosition;
            _graphic.color=new Color(_graphic.color.r,_graphic.color.g,_graphic.color.b,0);
            Vector3 targetPosition = _transform.position - Vector3.up * _distance;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_transform.DOMove(targetPosition, _appearDuration).SetEase(_easeIn));
            sequence.Join(_graphic.DOFade(1, _appearDuration));
            
            sequence.AppendInterval(_stayDuration);
            sequence.Join(_graphic.DOFade(0, _appearDuration));

        }
    }
}