using SpeedTextRPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTextRPG.Skills
{
    public class NormalSkillEffect : ISkillEffect
    {
        public float PowerRatio { get; set; } = 1.0f;
        public int RepeatCount { get; set; } = 1;
        public TargetType Target { get; set; }
        public AttributeType Attribute { get; set; }
        public string Description { get; set; }

        public void Apply(Character user, List<Character> targets)
        {
            foreach (var target in targets)
            {
                DamageInfo info = new(user, target, Attribute, PowerRatio, 0);
                Console.WriteLine($"{target.Name}에게 {Attribute} 피해 공격!");
                target.ReceiveDamage(info);
            }
        }
    }
}
