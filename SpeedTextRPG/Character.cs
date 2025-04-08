namespace SpeedTextRPG
{
    public abstract class Character
    {
        private readonly static float ACTIONPOINT_MAX_VALUE = 10000.0f;

        public readonly string Name;

        private int _level;

        private float _healthPoint;
        private float _healthMaxPoint;

        private float _attackPower;
        private float _defensePoint;

        private float _baseSpeed;
        public float AactionPoint;

        public Character(string name, int level, float healthPoint, float healthMaxPoint, float attackPower, float defensePoint, float baseSpeed)
        {
            Name = name;
            _level = level;
            _healthPoint = healthPoint;
            _healthMaxPoint = healthMaxPoint;
            _attackPower = attackPower;
            _defensePoint = defensePoint;
            _baseSpeed = baseSpeed;
        }

        // 배틀 시작 시
        public void BattleStartSetting()
        {
            AactionPoint = ACTIONPOINT_MAX_VALUE / _baseSpeed;
        }

        public abstract void TargetAttack(); // 일반 공격

        public abstract void EpicSkill(); // 특수 능력
        public abstract void SpecialSkill(); // 궁극기
    }
}
