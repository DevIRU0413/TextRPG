using SpeedTextRPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTextRPG.Skills.Blade
{
    public class BasicWindAttackEffect : ISkillEffect
    {
        public float PowerRatio { get; set; } = 0.5f;
        public string Description { get; set; } = "단일 대상에게 공격력의 50% 바람 피해";

        public void Apply(Character user, List<Character> targets)
        {
            var target = targets[0];
            float damage = user.AttackPower * PowerRatio;
            target.ReceiveDamage(damage, AttributeType.Wind);

            Console.WriteLine($"{target.Name}에게 {damage} 바람 피해 (샤드 소드)");
        }
    }
}
