using SpeedTextRPG.Buffs;
using SpeedTextRPG.Skills.SpeedTextRPG.Skills;

namespace SpeedTextRPG
{
    public class Character
    {
        public readonly string Name;
        public readonly AttributeType Attribute;
        public int Level { get; private set; }
        public float HealthPoint { get; private set; }
        public float HealthMaxPoint { get; private set; }
        public float AttackPower { get; private set; }
        public float DefensePoint { get; private set; }
        public float BaseSpeed { get; private set; }
        public float ActionPoint;
        public int turnCount; // debug

        public SkillBag SkillBag { get; private set; }
        public List<Buff> ActiveBuffs { get; private set; } = new();

        public Character(string name, AttributeType attribute, int level, float hp, float maxHp, float atk, float def, float speed, SkillBag skillBag)
        {
            Name = name;
            Attribute = attribute;

            Level = level;

            HealthPoint = hp;
            HealthMaxPoint = maxHp;
            AttackPower = atk;
            DefensePoint = def;
            BaseSpeed = speed;

            SkillBag = skillBag;
        }

        public override string ToString()
        {
            string result = $"이름: {Name} \n레벨: {Level} \n체력: {HealthPoint}/{HealthMaxPoint} \n공격력: {AttackPower} \n방어력: {DefensePoint} \n속도: {BaseSpeed}";
            return result;
        }

        public void ReceiveDamage(float damage, AttributeType attribute)
        {
            AttributeType weaknessAttribute = GetWeaknessAttribute(attribute);
            // 공격 받은 캐릭터가 가진 속성이 약점 속성일 경우, 데미지 * 1.2배
            if (Attribute == weaknessAttribute)
                damage *= 1.2f;

            float finalDamage = damage - DefensePoint;
            if (finalDamage < 0) finalDamage = 0;
            HealthPoint -= finalDamage;

            Console.WriteLine($"♥ {Name} 남은 체력: {HealthPoint}");
            if (HealthPoint <= 0)
                Console.WriteLine($"{Name} 이(가) 쓰러졌습니다!");
        }

        public void ApplyBuff(Buff newBuff)
        {
            var existing = ActiveBuffs.FirstOrDefault(b => b.Stat == newBuff.Stat && b.Type == newBuff.Type);

            if (existing != null)
            {
                if (existing.IsStackable && existing.CurrentStacks < existing.MaxStacks)
                {
                    existing.CurrentStacks++;
                    existing.Duration = newBuff.Duration;
                    Console.WriteLine($"{Name}의 '{existing.Name}' 중첩 → {existing.CurrentStacks}스택");
                }
                else
                {
                    existing.Duration = newBuff.Duration;
                    Console.WriteLine($"{Name}의 '{existing.Name}' 지속 시간 갱신됨.");
                }
            }
            else
            {
                ActiveBuffs.Add(newBuff);
                Console.WriteLine($"{Name}에게 '{newBuff.Name}' {(newBuff.Type == BuffType.Buff ? "버프" : "디버프")} 적용됨!");
            }

            RecalculateStats();
        }

        public void TickBuffs()
        {
            for (int i = ActiveBuffs.Count - 1; i >= 0; i--)
            {
                var buff = ActiveBuffs[i];
                buff.Duration--;

                if (buff.Duration <= 0)
                {
                    Console.WriteLine($"{Name}의 {buff.Stat} 버프가 해제되었습니다.");
                    ActiveBuffs.RemoveAt(i);
                }
            }

            RecalculateStats();
        }

        private void RecalculateStats()
        {
            // 기본 스탯 + 버프 적용
            float totalAtk = AttackPower;
            float totalDef = DefensePoint;
            float totalSpeed = BaseSpeed;

            foreach (var buff in ActiveBuffs)
            {
                switch (buff.Stat)
                {
                    case StatType.ATK:
                        totalAtk += buff.TotalAmount;
                        break;
                    case StatType.DEF:
                        totalDef += buff.TotalAmount;
                        break;
                    case StatType.SPD:
                        totalSpeed += buff.TotalAmount;
                        break;
                }
            }

            Console.WriteLine($"{Name} 현재 스탯 | ATK: {totalAtk}, DEF: {totalDef}, SPD: {totalSpeed}");
        }

        private AttributeType GetWeaknessAttribute(AttributeType attribute)
        {
            AttributeType weaknessAttribute = AttributeType.None;
            switch (attribute)
            {
                case AttributeType.Physical:
                    weaknessAttribute = AttributeType.Physical;

                    break;
                case AttributeType.Fire:
                    weaknessAttribute = AttributeType.Wind;
                    break;
                case AttributeType.Ice:
                    weaknessAttribute = AttributeType.Fire;
                    break;
                case AttributeType.Lightning:
                    weaknessAttribute = AttributeType.Ice;
                    break;
                case AttributeType.Wind:
                    weaknessAttribute = AttributeType.Lightning;
                    break;

                case AttributeType.Quantum:
                    weaknessAttribute = AttributeType.Imaginary;
                    break;
                case AttributeType.Imaginary:
                    weaknessAttribute = AttributeType.Quantum;
                    break;

                default:
                    Console.WriteLine("ERROR");
                    break;
            }
            return weaknessAttribute;
        }
    }
}
