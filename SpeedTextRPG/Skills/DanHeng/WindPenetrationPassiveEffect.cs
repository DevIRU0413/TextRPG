using SpeedTextRPG.Buffs;
using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills.DanHeng
{
    public class WindPenetrationPassiveEffect : ISkillEffect
    {
        public float PenetrationAmount = 0.18f;
        public int CooldownTurns = 2;
        public string Description { get; set; } = "아군 능력 대상 시, 다음 공격에 바람 저항 관통 +18% (2턴 간 재사용 불가)";

        private int cooldownRemaining = 0;
        private bool isBuffApplied = false;

        public void OnAllySkillTargeted(Character danHeng)
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

            danHeng.ApplyBuff(buff);
            isBuffApplied = true;
            cooldownRemaining = CooldownTurns;
            Console.WriteLine($"각자의 장점 발동! 바람 저항 관통 강화");
        }

        public void OnTurnStart()
        {
            if (cooldownRemaining > 0) 
                cooldownRemaining--;
        }

        public void Apply(Character user, List<Character> targets) { /* 자동 트리거 */ }
    }

}