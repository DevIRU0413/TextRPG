using SpeedTextRPG.Buffs;
using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills.DanHeng
{
    internal class EtherealDreamEffect : SkillEffect, ISkillActionable
    {
        public float BasePowerRatio { get; set; } = 2.4f; // 240%
        public float BonusMultiplier { get; set; } = 0.72f; // 72%
        public void Apply(Character user, List<Character> targets)
        {
            var target = targets[0];
            float finalRatio = BasePowerRatio;

            // 슬로우 디법 찾기
            bool isSlowed = false;
            foreach (Buff b in target.ActiveBuffs)
            {
                if (b.Type == BuffType.Debuff && b.Stat == StatType.SPD && b.Amount < 0)
                    isSlowed = true;
            }

            // 타겟 슬로우 시
            if (isSlowed)
            {
                finalRatio += BonusMultiplier;
                Console.WriteLine($"느려진 적 → 피해 배율 ({BasePowerRatio * 100}%)+{BonusMultiplier * 100}% 적용됨!");
            }

            target.ReceiveDamage(new(user,target,Attribute,finalRatio,0));
            Console.WriteLine($"{target.Name}에게 바람 피해! (동천환화, 기나긴 꿈)");
        }
    }
}