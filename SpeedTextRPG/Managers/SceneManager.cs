using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Managers
{
    public class SceneManager
    {
        private static SceneManager _instance;
        public static SceneManager Instance => _instance ??= new SceneManager();

        private readonly Dictionary<SceneType, BaseScene> _scenes = new Dictionary<SceneType, BaseScene>();
        private BaseScene _currentScene;

        public void RegisterScene(SceneType type, BaseScene scene)
        {
            if (!_scenes.ContainsKey(type))
                _scenes[type] = scene;
        }

        public void ChangeScene(SceneType sceneType)
        {
            // 현재 씬이 있다면, 기존 거 나가기 
            _currentScene?.Exit();
            _currentScene = _scenes[sceneType];

            // 현재 씬, 진입
            _currentScene.Enter();
            RenderCurrentScene();
        }

        public void RenderCurrentScene()
        {
            _currentScene?.Render();
        }

        public void UpdateCurrentScene()
        {
            _currentScene?.Update();
        }

        public void HandleInput(ConsoleKey key)
        {
            _currentScene?.HandleInput(key);
        }
    }
}