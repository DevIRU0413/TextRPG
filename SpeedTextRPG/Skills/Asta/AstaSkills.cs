using SpeedTextRPG.Skills.SpeedTextRPG.Skills;

namespace SpeedTextRPG.Skills.Asta
{
    public static class AstaSkills
    {
        public static Skill SpectrumBeam => new Skill
        {
            Name = "스펙트럼 빔",
            Level = 6,
            Type = SkillType.Normal,
            Tag = SkillTag.Attack,
            EnergyGain = 20,
            Cooldown = 10,
            Effect = new NormalSkillEffect
            {
                Attribute = AttributeType.Fire,
                Target = TargetType.SingleEnemy,
                PowerRatio = 1.0f,
                Description = "적 하나에게 아스타 공격력의 100% 화염 피해"
            }
        };

        public static Skill MeteorStorm => new Skill
        {
            Name = "유성 폭풍",
            Level = 10,
            Type = SkillType.Special,
            Tag = SkillTag.Attack,
            EnergyGain = 6,
            Cooldown = 10,
            Effect = new MeteorStormEffect
            {
                Attribute = AttributeType.Fire,
                Target = TargetType.RandomEnemies,
                Description = "단일 적에게 아스타 ATK의 50% 화염 피해 + 무작위 적 4회 추가 타격"
            }
        };

        public static Skill AstralBlessing => new Skill
        {
            Name = "아스트랄 축복",
            Level = 10,
            Type = SkillType.Ultimate,
            Tag = SkillTag.Support,
            EnergyGain = 5,
            EnergyCost = 120,
            Effect = new SpeedBuffEffect
            {
                BuffAmount = 50f,
                Duration = 2,
                Target = TargetType.AllAllies,
                Attribute = AttributeType.None,
                Description = "2턴간 모든 아군의 SPD +50"
            }
        };

        public static Skill Astrometry => new Skill
        {
            Name = "천체 측정학",
            Level = 10,
            Type = SkillType.Talent,
            Tag = SkillTag.Passive,
            EnergyGain = 0,
            Effect = new AstrometryPassiveEffect
            {
                Attribute = AttributeType.Fire,
                Target = TargetType.AllAllies,
                Description = "공격한 적마다 중첩 증가, 턴마다 감소, 아군 ATK 강화"
            }
        };

        public static Skill MiracleFlash => new Skill
        {
            Name = "기적의 플래시",
            Level = 1,
            Type = SkillType.Technique,
            Tag = SkillTag.Attack,
            EnergyGain = 0,
            Cooldown = 20,
            Effect = new FlashEntryEffect
            {
                Attribute = AttributeType.Fire,
                Target = TargetType.AllEnemies,
                Description = "전투 진입 시 모든 적에게 아스타 ATK의 50% 화염 피해"
            }
        };

        public static SkillBag ToSkillBag() => new SkillBag(
            SpectrumBeam,
            MeteorStorm,
            AstralBlessing,
            Astrometry,
            MiracleFlash
        );
    }
}