using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills.Asta
{
    public class FlashEntryEffect : SkillEffect, ISkillActionable
    {
        public float PowerRatio { get; set; } = 0.5f;
        public void Apply(Character user, List<Character> targets)
        {
            var enemies = BattleManager.Instance.GetAllEnemies(user);
            foreach (var enemy in enemies)
            {
                DamageInfo info = new DamageInfo(user, enemy, Attribute, PowerRatio, 0);
                enemy.ReceiveDamage(info);
                Console.WriteLine($"{enemy.Name}에게 {Attribute} 피해! (기적의 플래시)");
            }
        }
    }
}