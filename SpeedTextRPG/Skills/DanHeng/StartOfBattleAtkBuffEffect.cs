using SpeedTextRPG.Buffs;
using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills.DanHeng
{
    public class StartOfBattleAtkBuffEffect : ISkillEffect
    {
        public float AtkBuffPercent = 0.4f;
        public int Duration = 3;
        public string Description { get; set; } = "전투 시작 시 3턴 ATK +40%";

        public void Apply(Character character, List<Character> targets)
        {
            float buffAmount = character.AttackPower * AtkBuffPercent;

            var buff = new Buff
            {
                Name = "섬멸의 칼날",
                Description = $"ATK +{AtkBuffPercent * 100}% (3턴)",
                Stat = StatType.ATK,
                Amount = buffAmount,
                Duration = Duration,
                Type = BuffType.Buff,
                IsStackable = false
            };

            character.ApplyBuff(buff);
        }
    }

}