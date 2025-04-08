namespace SpeedTextRPG
{
    public class Game
    {
        // Text RPG Loop
        private bool _gameOver = false;
        private ConsoleKey _inputKey;

        private Player _player; // 플레이어 정보
        private Shop _shop;

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
            _player = new("DevIRU", 10, 10, 2, 33);
            _shop = new Shop();
        }
        private void Start()
        {
            _player.Print();
            _shop.PrintPotions();
        }
        private void Render()
        {
            Console.Clear();
            Potion buyPotion = _shop.Shoping(_player);
            if (buyPotion == null) return;

            switch (buyPotion.Name)
            {
                case "체력 회복 포션":
                    _player.HealthMaxPoint += buyPotion.PotionPoint;
                    break;
                case "공격 상승 포션":
                    _player.AttackPower += buyPotion.PotionPoint;
                    break;
                case "방어력 상승 포션":
                    _player.DefensePoint += buyPotion.PotionPoint;
                    break;
            }
        }
        private void Input()
        {
            _inputKey = ConsoleKey.None;
            _inputKey = Console.ReadKey(true).Key;
        }
        private void Update()
        {

        }
    }
}