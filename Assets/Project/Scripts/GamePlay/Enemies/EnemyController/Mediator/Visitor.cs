using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.Mediator
{
    public interface IVisitor
    {
        void Visit<T>(T visitable) where T :Component, IVisitable;
    }

    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}