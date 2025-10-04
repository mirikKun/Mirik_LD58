using System;
using Assets.Code.GamePlay.Stats;
using ImprovedTimers;
using ImprovedTimers.Project.Scripts.Utils.Timers;
using UnityEngine;

public class BasicStatModifier : StatModifier
{
    private readonly StatType _type;
    private readonly Func<float, float> _operation;

    public BasicStatModifier(StatType type, float duration, Func<float, float> operation) : base(duration)
    {
        this._type = type;
        this._operation = operation;
    }

    public override void Handle(object sender, Query query)
    {
        if (query.StatType == _type)
        {
            query.Value = _operation(query.Value);
        }
    }
}

public abstract class StatModifier : IDisposable
{
    public bool MarkedForRemoval { get; set; }

    public event Action<StatModifier> OnDispose = delegate { };

    readonly CountdownTimer _timer;

    protected StatModifier(float duration)
    {
        if (duration <= 0) return;

        _timer = new CountdownTimer(duration);
        _timer.OnTimerStop += () => MarkedForRemoval = true;
        _timer.Start();
    }

    public void Update(float deltaTime) => _timer?.Tick(deltaTime);

    public abstract void Handle(object sender, Query query);

    public void Dispose()
    {
        OnDispose.Invoke(this);
    }
}