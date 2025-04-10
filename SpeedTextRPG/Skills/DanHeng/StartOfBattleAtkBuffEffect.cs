using SpeedTextRPG.Buffs;
using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills.DanHeng
{
    public class StartOfBattleAtkBuffEffect : SkillEffect, ISkillActionable
    {
        public float AtkBuffPercent = 0.4f;
        public int Duration = 3;

        public void Apply(Character user, List<Character> targets)
        {
            var buff = new Buff
            {
                Name = "섬멸의 칼날",
                Description = $"ATK +{AtkBuffPercent * 100}% (3턴)",
                Stat = StatType.ATK,
                Amount = AtkBuffPercent,
                Duration = Duration,
                Type = BuffType.Buff,
                IsStackable = false
            };

            user.ApplyBuff(buff);
        }
    }

}