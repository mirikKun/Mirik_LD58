using UnityEngine;

namespace Project.Scripts.GamePlay.Armaments.ArmamentBehaviour.Abstract
{
    public abstract class ComponentBehaviour:MonoBehaviour,IArmamentBehaviour
    {
        public abstract void InitArmament(Armament armament);
    }
}