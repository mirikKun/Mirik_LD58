using System;

using Assets.Code.GamePlay.Common.Entity;
using UnityEngine;


namespace Assets.Code.GamePlay.Enemies.EnemyController
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator:EntityComponent
    {


        private Animator _animator;
        private const float CrossFadeDuration = 0.2f;
        private int _currentAnimationHash;
        public event Action<int> StateEntered;
        public event Action<int> StateExited;
        public event Action<string> OnAnimationEvent;
        public int CurrentAnimationHash => _currentAnimationHash;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void StartAnimation(int  animationHash,float crossFadeDuration=-1)
        {
            float duration = crossFadeDuration < 0 ? CrossFadeDuration : crossFadeDuration;
            _animator.CrossFade(animationHash, duration);
        }

        public void SetHeadTarget(Transform target)
        {

            
            
            _animator.enabled = false;
            _animator.enabled = true;

        }

        public void ClearHeadTarget()
        {

            _animator.enabled = false;
            _animator.enabled = true;        }

        public bool AnimationEnded()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
        }

        public void EnteredState(int stateHash)
        {
            _currentAnimationHash = stateHash;
            StateEntered?.Invoke(stateHash);
        }

        public void ExitedState(int stateHash)
        {
            StateExited?.Invoke(stateHash);
        }

        public void OnAnimationEventTriggered(string eventName)
        {
            OnAnimationEvent?.Invoke(eventName);
        }
    }
}