namespace SpeedTextRPG
{
    public class Player
    {
        private readonly string _name; // 플레이어 파티의 이름
        private int _haveGold; // 플레이어의 소지금

        public CharacterGroup Group { get; private set; }

        public Player(string playerName, string groupName, int haveGold = 0)
        {
            _name = playerName;
            Group = new(groupName);
            _haveGold = haveGold;
        }

        // 소지금 사용
        public bool UseHaveGold(int useGold)
        {
            if (_haveGold < useGold) return false;

            _haveGold -= useGold;
            return true;
        }
        // 소지금 획득
        public void GetGold(int getGold)
        {
            _haveGold += getGold;
            if (_haveGold < 0)
                _haveGold = 0;
        }

        // 몬스터와 충돌 시
        public void BattleEncounter(CharacterGroup targetGroup)
        {
            BattleManager.Instance.Battle(Group, targetGroup);
        }
    }
}