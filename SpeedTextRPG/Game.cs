using SpeedTextRPG.Managers;

namespace SpeedTextRPG
{
    public class Game
    {
        private bool _gameOver = false;
        private ConsoleKey _inputKey;

        private Player _player; // 플레이어 정보

        public void Play()
        {
            Init();
            Start();
            while (!_gameOver)
            {
                Render();
                Input();
                Update();
            }
        }

        // 전체적인 데이터 초기화
        private void Init()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode; // UTF-16 출력
        }

        // 데이터 로딩 같은 느낌으로 사용
        private void Start()
        {
            _player = new("DevIRU", "LOGGER");
            _player.Group.SetCharacter(CharacterFactory.Instance.Create(CharacterId.Asta));
            _player.Group.SetCharacter(CharacterFactory.Instance.Create(CharacterId.DanHeng));

            // Player를 넘겨서 BattleScene 초기화
            SceneManager.Instance.RegisterScene(SceneType.Title, new TitleScene());
            SceneManager.Instance.RegisterScene(SceneType.Battle, new BattleScene(_player));

            SceneManager.Instance.ChangeScene(SceneType.Title);
        }

        // 그림 그리기 UI 느낌
        private void Render()
        {
            SceneManager.Instance.RenderCurrentScene();
        }

        private void Input()
        {
            _inputKey = Console.ReadKey(true).Key;
            SceneManager.Instance.HandleInput(_inputKey);
        }

        private void Update()
        {
            SceneManager.Instance.UpdateCurrentScene();
        }
    }
}