using UnityEngine;

namespace Assets.Code.GamePlay.Enemies.EnemyController.Mediator
{
    public abstract class Payload<TData>:IVisitor
    {
        public abstract TData Content { get; set; }
        public abstract void Visit<T>(T visitable) where T : Component, IVisitable;
    }
    
    public class EnemyDetectPayload:Payload<bool>
    {
        public EnemyEntity Source { get; set; }
        public override bool Content { get; set; }
        public EnemyDetectPayload() { }
        public override void Visit<T>(T visitable)
        {
            Debug.Log($"{visitable.name} received message from {Source.name}: {Content}");
            EnemyEntity target = visitable as EnemyEntity;
            target.Get<CharacterDetector>().SetCharacterDetectByOther(Content);
        }

        public class Builder
        {
            private EnemyDetectPayload _payload = new EnemyDetectPayload();
            public Builder(EnemyEntity source) => _payload.Source = source;

            public Builder WithContent(bool content)
            {
                _payload.Content = content;
                return this;
            }
            public EnemyDetectPayload Build() => _payload;
        }
    }
    
    
    public class MessagePayload:Payload<string>
    {
        public EnemyEntity Source { get; set; }
        public override string Content { get; set; }
        public MessagePayload() { }
        public override void Visit<T>(T visitable)
        {
            Debug.Log($"{visitable.name} received message from {Source.name}: {Content}");
        }

        public class Builder
        {
            private MessagePayload _payload = new MessagePayload();
            public Builder(EnemyEntity source) => _payload.Source = source;

            public Builder WithContent(string content)
            {
                _payload.Content = content;
                return this;
            }
            public MessagePayload Build() => _payload;
        }
    }
}