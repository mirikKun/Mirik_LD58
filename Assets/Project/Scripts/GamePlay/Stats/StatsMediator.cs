using System;
using System.Collections.Generic;
using Assets.Code.GamePlay.Stats;
using UnityEngine;

public class StatsMediator
{
    readonly LinkedList<StatModifier> modifiers = new();

    public event EventHandler<Query> Queries;

    public void PerformQuery(object sender, Query query) => Queries?.Invoke(sender, query);

    public void AddModifier(StatModifier modifier)
    {        Debug.Log("StatsMediator: Adding modifier " + modifier);

        modifiers.AddLast(modifier);
        modifier.MarkedForRemoval = false;
        Queries += modifier.Handle;

        modifier.OnDispose += _ =>
        {
            modifiers.Remove(modifier);
            Queries -= modifier.Handle;
        };
    }

    public void Tick(float deltaTime)
    {
        var node = modifiers.First;
        while (node != null)
        {
            var modifier = node.Value;
            modifier.Update(deltaTime);
            node = node.Next;
        }


        node = modifiers.First;
        while (node != null)
        {
            var nextNode = node.Next;

            if (node.Value.MarkedForRemoval)
            {
                node.Value.Dispose();
            }

            node = nextNode;
        }
    }
}

public class Query
{
    public readonly StatType StatType;
    public float Value;

    public Query(StatType statType, float value)
    {
        StatType = statType;
        Value = value;
    }
}