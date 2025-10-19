using Assets.Code.GamePlay.Common.Entity;
using FMOD.Studio;
using FMODUnity;
using Project.Scripts.Sounds;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Project.Scripts.GamePlay.Player.PlayerEffects
{
    public class BookEffects : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particles;
        [SerializeField] private Transform _book;
        [SerializeField] private Vector3 _stealingRotation;
        [SerializeField] private float _bookRotationSpeed = 5f;

        [SerializeField] private ParticleSystem _stealSuccessParticles;
        [SerializeField] private ParticleSystem _firstStealParticles;
        [Header("Sounds")] 
        [SerializeField] public EventReference _processSound;
        [SerializeField] public EventReference _hitSound;
        private Vector3 _startRotation;
        private Vector3 _targetRotation;
        private ActorEntity _entity;
        private EventInstance _absorbingSound;


        public void InitEffect(ActorEntity entity)
        {
            _entity = entity;
            _startRotation = _book.localEulerAngles;
            _targetRotation = _startRotation;
            _absorbingSound=_entity.Get<SoundSource>().CreateInstance(_processSound);
        }

  
        public void Tick(float deltaTime)
        {
            _book.localEulerAngles = Vector3.Lerp(_book.localEulerAngles, _targetRotation, deltaTime * _bookRotationSpeed);
        }

        public void StartStealingEffect()
        {
            _particles.Play();
            _targetRotation = _stealingRotation;
            _absorbingSound.start();
        }

        public void StopStealingEffect()
        {
            _particles.Stop();
            _targetRotation = _startRotation;
            _absorbingSound.stop(STOP_MODE.ALLOWFADEOUT);

        }

        public void PlayStealSuccessEffect()
        {
            _stealSuccessParticles.Play();
            _entity.Get<SoundSource>().PlaySound(_hitSound,SoundPlacementType.LeftHand);
        }

        public void PlayFirstStealEffect()
        {
            _firstStealParticles.Play();
        }
    }
}