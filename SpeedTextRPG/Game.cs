using SpeedTextRPG.Managers;
using SpeedTextRPG.Skills;
using SpeedTextRPG.Skills.Asta;

namespace SpeedTextRPG
{
    public class Game
    {
        // Text RPG Loop
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
        private void Init()
        {
            _player = new("DevIRU", "LOGGER");

            Character a = CharacterFactory.Instance.Create(CharacterId.DanHeng);
            Character h = CharacterFactory.Instance.Create(CharacterId.Asta);

            CharacterGroup monster = new("Test");
            _player.Group.SetCharacter(a);
            monster.SetCharacter(h);

            _player.BattleEncounter(monster);
        }
        private void Start()
        {
        }
        private void Render()
        {
            Console.Clear();
            BattleManager.Instance.Print();
        }
        private void Input()
        {
            _inputKey = ConsoleKey.None;
            _inputKey = Console.ReadKey(true).Key;
            BattleManager.Instance.NextTurn();
            _inputKey = Console.ReadKey(true).Key;
        }
        private void Update()
        {

        }
    }
}