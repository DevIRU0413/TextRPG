using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills.Asta
{
    public class MeteorStormEffect : ISkillEffect
    {
        public float PowerRatio { get; set; } = 0.5f;
        public int RandomAttackCount { get; set; } = 4;

        public AttributeType Attribute { get; set; } = AttributeType.Fire;
        public TargetType Target { get; set; } = TargetType.RandomEnemies;
        public string Description { get; } = "지정 적 1 + 무작위 적 4회 화염 피해";

        public void Apply(Character user, List<Character> targets)
        {
            if (targets == null || targets.Count == 0) return;

            // 첫 번째 대상: 명확히 지정된 대상
            Character mainTarget = targets[0];
            DamageInfo info = new(user, mainTarget, Attribute, PowerRatio, 0);
            Console.WriteLine($"{mainTarget.Name}에게 화염 피해! (유성 폭풍 - 첫 타격)");
            mainTarget.ReceiveDamage(info);

            List<Character> allEnemies = BattleManager.Instance.GetAllEnemies(user);
            if (allEnemies.Count == 0) return;

            // 무작위 대상
            Random rnd = new();
            for (int i = 0; i < 4; i++)
            {
                var randomTarget = allEnemies[rnd.Next(allEnemies.Count)];
                DamageInfo infoR = new(user, randomTarget, Attribute, PowerRatio, 0);

                float hit = user.AttackPower * PowerRatio;
                randomTarget.ReceiveDamage(infoR);

                Console.WriteLine($"{randomTarget.Name}에게 {hit} 화염 피해! (유성 폭풍 - 무작위 {i + 1}회)");
            }
        }
    }
}
