using System;
using Assets.Code.Common.Utils.ActionList;
using Code.Gameplay.Common.Time;
using UnityEngine;
using Zenject;

namespace Assets.Code.GamePlay.DataDriven.Statuses
{
    public abstract class BaseStatus:IActionElement
    {
        public abstract void Apply();
        public abstract void Stop();
    }
    
    
}