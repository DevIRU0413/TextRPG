using SpeedTextRPG.Buffs;
using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills.Asta
{
    public class AstrometryPassiveEffect : SkillEffect, ISkillAttackTrigger, ISkillTurnTrigger
    {
        private int stacks = 0;

        private const int MaxStacks = 5;
        private const float AttackBuffPerStack = 14f;

        // Attack
        public void OnAttack(Character user, Character target)
        {
            stacks++;
            if (target.WeaknessAttribute == AttributeType.Fire) stacks++; // 화염 약점 추가 중첩
            if (stacks > MaxStacks) stacks = MaxStacks;

            Console.WriteLine($"아스타 충전 중첩: {stacks} → 전체 아군 ATK 증가");
            ApplyBuffToAllies(user);
        }

        // Turn
        public void OnTurnStart(Character user)
        {
            if (stacks > 0)
            {
                stacks = Math.Max(0, stacks - 3);
                Console.WriteLine($"아스타 충전 중첩 감소 → {stacks}");
                ApplyBuffToAllies(user);
            }
        }
        public void OnTurnEnd(Character user)
        {
        }

        private void ApplyBuffToAllies(Character user)
        {
            List<Character> allies = BattleManager.Instance.GetAllAllies(user);
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
    }
}