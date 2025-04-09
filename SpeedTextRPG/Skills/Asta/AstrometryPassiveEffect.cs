using SpeedTextRPG.Buffs;
using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills.Asta
{
    public class AstrometryPassiveEffect : ISkillEffect
    {
        public string Description { get; set; }

        public AttributeType Attribute => throw new NotImplementedException();

        public TargetType Target => throw new NotImplementedException();

        private int stacks = 0;

        private const int MaxStacks = 5;
        private const float AttackBuffPerStack = 14f;

        // 공격 시
        public void OnAttack(Character character, Character target)
        {
            stacks++;
            if (target.WeaknessAttribute == AttributeType.Fire) stacks++; // 화염 약점 추가 중첩
            if (stacks > MaxStacks) stacks = MaxStacks;

            Console.WriteLine($"아스타 충전 중첩: {stacks} → 전체 아군 ATK 증가");
            ApplyBuffToAllies(character);
        }

        // 턴 종료 시
        public void OnTurnStart(Character character)
        {
            if (stacks > 0)
            {
                stacks = Math.Max(0, stacks - 3);
                Console.WriteLine($"아스타 충전 중첩 감소 → {stacks}");
                ApplyBuffToAllies(character);
            }
        }

        private void ApplyBuffToAllies(Character character)
        {
            var allies = BattleManager.Instance.GetAllEnemies(character);
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