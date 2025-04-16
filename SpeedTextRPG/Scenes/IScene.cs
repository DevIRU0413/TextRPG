namespace SpeedTextRPG.Scenes
{
    interface IScene
    {
        void Init();
        void Update();
        void Render();
        bool IsDone { get; }
        IScene GetNextScene(); // 다음 씬 반환
    }
}
