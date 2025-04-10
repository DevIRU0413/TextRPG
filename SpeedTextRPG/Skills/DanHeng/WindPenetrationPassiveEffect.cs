using SpeedTextRPG.Buffs;
using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills.DanHeng
{
    public class WindPenetrationPassiveEffect : SkillEffect, ISkillTurnTrigger, IAllySkillTargetedTrigger
    {
        public float PenetrationAmount = 0.18f;
        public int CooldownTurns = 2;

        private int cooldownRemaining = 0;

        // 스킬 타겟으로 지정 될 시
        public void OnAllySkillTargeted(Character user)
        {
            if (cooldownRemaining > 0)
            {
                Console.WriteLine($"패시브 쿨다운: {cooldownRemaining}턴 남음");
                return;
            }

            Buff buff = new Buff
            {
                Name = "바람 저항 관통",
                Description = $"다음 공격 바람 관통 +{PenetrationAmount * 100}%",
                Stat = StatType.None, 
                Amount = PenetrationAmount,
                Duration = 1,
                Type = BuffType.Buff
            };

            user.ApplyBuff(buff);
            cooldownRemaining = CooldownTurns;
            Console.WriteLine($"각자의 장점 발동! 바람 저항 관통 강화");
        }

        public void OnTurnStart(Character user)
        {
            if (cooldownRemaining > 0)
                cooldownRemaining--;
        }

        public void OnTurnEnd(Character user) { }
    }
}