using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills.Asta
{
    public class FlashEntryEffect : ISkillEffect
    {
        public float PowerRatio { get; set; } = 0.5f;
        public AttributeType Attribute { get; set; } = AttributeType.Fire;
        public string Description { get; set; }

        public void Apply(Character character, List<Character> targets)
        {
            var enemies = BattleManager.Instance.GetAllEnemies(character);
            foreach (var enemy in enemies)
            {
                float damage = character.AttackPower * PowerRatio;
                enemy.ReceiveDamage(damage, Attribute);
                Console.WriteLine($"{enemy.Name}에게 {damage}의 {Attribute} 피해! (기적의 플래시)");
            }
        }
    }

}