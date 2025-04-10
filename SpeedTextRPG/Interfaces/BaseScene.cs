namespace SpeedTextRPG.Interfaces
{
    public abstract class BaseScene
    {
        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Render() { }
        public virtual void Update() { }
        public virtual void HandleInput(ConsoleKey key) { }
    }
}
