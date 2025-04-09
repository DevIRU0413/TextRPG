using SpeedTextRPG.Buffs;
using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills.DanHeng
{
    internal class EtherealDreamEffect : ISkillEffect
    {
        public float BasePowerRatio { get; set; } = 2.4f; // 240%
        public float BonusMultiplier { get; set; } = 0.72f; // 72%
        public string Description { get; set; } = "ATK 240% 바람 피해 + 대상이 느려졌으면 +72% 피해 증가";

        public void Apply(Character user, List<Character> targets)
        {
            var target = targets[0];
            float finalRatio = BasePowerRatio;

            bool isSlowed = false;
            foreach (Buff b in target.ActiveBuffs)
            {
                if (b.Type == BuffType.Debuff && b.Stat == StatType.SPD && b.Amount < 0)
                    isSlowed = true;
            }

            if (isSlowed)
            {
                finalRatio += BonusMultiplier;
                Console.WriteLine($"느려진 적 → 피해 배율 ({BasePowerRatio * 100}%)+{BonusMultiplier * 100}% 적용됨!");
            }

            float damage = user.AttackPower * finalRatio;
            target.ReceiveDamage(damage, AttributeType.Wind);

            Console.WriteLine($"{target.Name}에게 {damage} 바람 피해! (동천환화, 기나긴 꿈)");
        }
    }
}