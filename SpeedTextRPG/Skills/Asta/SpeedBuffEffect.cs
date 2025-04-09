using SpeedTextRPG.Buffs;
using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills.Asta
{
    public class SpeedBuffEffect : ISkillEffect
    {
        public float BuffAmount { get; set; } = 0f;
        public int Duration { get; set; } = 1;
        public TargetType Target { get; set; } = TargetType.AllAllies;
        public string Description { get; set; } = "SPD 증가 버프";

        public void Apply(Character user, List<Character> targets)
        {
            foreach (var target in targets)
            {
                var speedBuff = new Buff
                {
                    Name = "SPD 증가",
                    Description = $"{Duration}턴 동안 SPD +{BuffAmount}",
                    Stat = StatType.SPD,
                    Amount = BuffAmount,
                    Duration = Duration,
                    Type = BuffType.Buff,
                    IsStackable = false
                };

                target.ApplyBuff(speedBuff);
            }
        }
    }
}