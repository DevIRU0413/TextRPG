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

        public AttributeType WeaknessAttribute => GetWeaknessAttribute(Attribute);

        public SkillBag SkillBag { get; private set; }
        public List<Buff> ActiveBuffs { get; private set; } = new();

        // 생성자
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

        // Buff
        public void ApplyBuff(Buff newBuff)
        {
            Buff existing = null;

            foreach (Buff buff in ActiveBuffs)
            {
                if (buff.Stat == newBuff.Stat && buff.Type == newBuff.Type)
                {
                    existing = buff;
                    break;
                }
            }

            // 이미 있는 경우
            if (existing != null)
            {
                // 스택 가능하고, 최대 스택에 넘지 않을때
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
            // 없는 경우
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
                    Console.WriteLine($"{Name}의 [{buff.Name}({buff.Stat})] 버프가 해제되었습니다.");
                    ActiveBuffs.RemoveAt(i);
                }
            }

            RecalculateStats();
        }


        private void RecalculateStats()
        {
            Console.WriteLine($"{Name} 현재 스탯 | ATK: {GetCurrentAttack()}, DEF: {GetCurrentDefense()}, SPD: {GetCurrentSpeed()}");
        }

        public float GetCurrentAttack()
        {
            float totalAtk = AttackPower;
            foreach (var buff in ActiveBuffs)
                if (buff.Stat == StatType.ATK) totalAtk += buff.TotalAmount;
            return totalAtk;
        }
        public float GetCurrentDefense()
        {
            float totalDef = DefensePoint;
            foreach (var buff in ActiveBuffs)
                if (buff.Stat == StatType.DEF) totalDef += buff.TotalAmount;
            return totalDef;
        }
        public float GetCurrentSpeed()
        {
            float totalSpeed = BaseSpeed;
            foreach (var buff in ActiveBuffs)
                if (buff.Stat == StatType.SPD) totalSpeed += buff.TotalAmount;
            return totalSpeed;
        }

        // Default
        public void ReceiveDamage(DamageInfo info)
        {
            float damage = info.FinalDamage;
            HealthPoint -= damage;
            Console.WriteLine($"{Name}이(가) {damage} 피해를 입음! (남은 체력: {HealthPoint})");
            if (HealthPoint <= 0)
                Console.WriteLine($"{Name} 이(가) 쓰러졌습니다!");
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
        public override string ToString()
        {
            return $"이름: {Name} \n레벨: {Level} \n체력: {HealthPoint}/{HealthMaxPoint} \n공격력: {AttackPower} \n방어력: {DefensePoint} \n속도: {BaseSpeed}";
        }
    }
}
