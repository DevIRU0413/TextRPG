using SpeedTextRPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTextRPG.Skills.Asta
{
    public class MeteorStormEffect : ISkillEffect
    {
        public float PowerRatio { get; set; } = 0.5f;
        public AttributeType Attribute { get; set; } = AttributeType.Fire;
        public string Description { get; set; }

        public void Apply(Character character, List<Character> targets)
        {
            if (targets == null || targets.Count == 0) return;

            // 첫 번째 대상: 명확히 지정된 대상
            Character mainTarget = targets[0];
            float damage = character.AttackPower * PowerRatio;
            mainTarget.ReceiveDamage(damage, AttributeType.Fire);

            Console.WriteLine($"{mainTarget.Name}에게 {damage} 화염 피해! (유성 폭풍 첫 타격)");

            List<Character> allEnemies = BattleManager.Instance.GetAllEnemies(character);
            Random rng = new();
            for (int i = 0; i < 4; i++)
            {
                if (allEnemies.Count == 0) break;

                var randomTarget = allEnemies[rng.Next(allEnemies.Count)];
                float hit = character.AttackPower * PowerRatio;
                randomTarget.ReceiveDamage(hit, AttributeType.Fire);

                Console.WriteLine($"{randomTarget.Name}에게 {hit} 화염 피해! (유성 폭풍 반사 {i + 1}회)");
            }
        }
    }
}
