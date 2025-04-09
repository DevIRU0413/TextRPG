namespace SpeedTextRPG
{
    public class DamageInfo
    {
        public Character User { get; private set; }
        public Character Target { get; private set; }

        public AttributeType Attribute { get; private set; } // 속성
        public float BasePower { get; private set; } // 공격력 기반 수치
        public float PowerRatio { get; private set; } = 1.0f; // 배율
        public float PenetrationRate { get; private set; } = 0f; // 방어 무시율

        public bool IsCritical { get; private set; } = false;

        public DamageInfo(Character user, Character target, AttributeType attribute, float powerRatio, float pentrationRate)
        {
            User = user;
            Target = target;
            Attribute = attribute;

            BasePower = User.AttackPower;
            PowerRatio = powerRatio;
            PenetrationRate = pentrationRate;

            Random random = new Random();
            IsCritical = (0.3 < random.NextDouble());
        }

        // 확인용
        public float FinalDamage => Calculate();

        private float Calculate()
        {
            // 공격력 * 스킬 배율
            float raw = BasePower;

            // 약점 속성 보정
            AttributeType weakness = GetWeaknessAttribute(Attribute);
            if (Target.Attribute == weakness)
                raw *= (PowerRatio + 1.2f);
            else
                raw *= PowerRatio;

            // 크리티컬 처리
            raw *= (IsCritical) ? 1.5f : 1.0f;

            // 방어력 관통 처리
            float adjustedDef = Target.DefensePoint * (1 - PenetrationRate);
            float result = raw - adjustedDef;
            return Math.Max(result, 0);
        }
        private AttributeType GetWeaknessAttribute(AttributeType attr)
        {
            // 약점 처리 로직
            return attr switch
            {
                AttributeType.Physical => AttributeType.Physical,
                AttributeType.Fire => AttributeType.Wind,
                AttributeType.Ice => AttributeType.Fire,
                AttributeType.Lightning => AttributeType.Ice,
                AttributeType.Wind => AttributeType.Lightning,
                AttributeType.Quantum => AttributeType.Imaginary,
                AttributeType.Imaginary => AttributeType.Quantum,
                _ => AttributeType.None,
            };
        }
    }

}
