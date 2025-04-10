using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills
{
    public class NormalSkillEffect : SkillEffect, ISkillActionable
    {
        public float PowerRatio { get; set; } = 1.0f;
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