using SpeedTextRPG.Buffs;
using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills.Asta
{
    public class AstrometryPassiveEffect : ISkillEffect
    {
        public string Description { get; set; }
        private int stacks = 0;

        private const int MaxStacks = 5;
        private const float AttackBuffPerStack = 14f;

        public void OnAttack(Character user, Character target)
        {
            stacks++;
            if (target.Attribute == AttributeType.Fire) stacks++; // 화염 약점 추가 중첩

            if (stacks > MaxStacks) stacks = MaxStacks;

            Console.WriteLine($"아스타 충전 중첩: {stacks} → 전체 아군 ATK 증가");
            ApplyBuffToAllies(user);
        }

        public void OnTurnStart(Character user)
        {
            if (stacks > 0)
            {
                stacks = Math.Max(0, stacks - 3);
                Console.WriteLine($"아스타 충전 중첩 감소 → {stacks}");
                ApplyBuffToAllies(user);
            }
        }

        private void ApplyBuffToAllies(Character user)
        {
            var allies = BattleManager.Instance.GetAllEnemies(user);
            foreach (var ally in allies)
            {
                float buffAmount = stacks * AttackBuffPerStack;
                ally.ApplyBuff(
                    new Buff
                    {
                        Name = "천체 충전 버프",
                        Description = $"중첩 {stacks}로 인한 ATK +{buffAmount}",
                        Stat = StatType.ATK,
                        Amount = buffAmount,
                        Duration = 1, // 매 턴 갱신됨
                        Type = BuffType.Buff,
                        IsStackable = false
                    }
                );
            }
        }

        public void Apply(Character user, List<Character> targets) { /* 자동 트리거 방식 */ }
    }

}