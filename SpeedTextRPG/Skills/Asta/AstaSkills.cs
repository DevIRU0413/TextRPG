using SpeedTextRPG.Skills.SpeedTextRPG.Skills;

namespace SpeedTextRPG.Skills.Asta
{
    public static class AstaSkills
    {
        public static Skill NormalSkill => new Skill
        {
            Name = "스펙트럼 빔",
            Level = 1,
            Type = SkillType.Normal,
            Tag = SkillTag.Attack,
            EnergyGain = 20,
            Cooldown = 10,
            Effect = new NormalSkillEffect
            {
                PowerRatio = 1.0f,
                RepeatCount = 1,
                Target = TargetType.SingleEnemy,
                Attribute = AttributeType.Fire,
                Description = "단일 대상에게 아스타 공격력의 100% 화염 피해"
            }
        };
        public static Skill SpecialSkill => new Skill
        {
            Name = "메테오 스톰",
            Level = 1,
            Type = SkillType.Special,
            Tag = SkillTag.Attack,
            EnergyGain = 6,
            Cooldown = 10,
            Effect = new MeteorStormEffect
            {
                PowerRatio = 0.5f,
                Attribute = AttributeType.Fire,
            }
        };
        public static Skill UltimateSkill => new Skill
        {
            Name = "별하늘의 축언",
            Level = 1,
            Type = SkillType.Ultimate,
            Tag = SkillTag.Support,
            EnergyGain = 5,
            EnergyCost = 120,
            Effect = new SpeedBuffEffect
            {
                BuffAmount = 50f,
                Duration = 2,
                Target = TargetType.AllAllies,
                Description = "2턴간 모든 아군의 SPD +50"
            }
        };
        public static Skill TalentSkill => new Skill
        {
            Name = "천체학",
            Level = 10,
            Type = SkillType.Talent,
            Tag = SkillTag.Passive,
            Effect = new AstrometryPassiveEffect
            {
                Description = "충전 중첩 기반 아군 공격력 +14% (최대 5스택), 매 턴 3감소"
            }
        };

        public static Skill TechniqueSkill => new Skill
        {
            Name = "미라클 플래쉬",
            Level = 1,
            Type = SkillType.Technique,
            Tag = SkillTag.Attack,
            Cooldown = 20,
            Effect = new FlashEntryEffect
            {
                PowerRatio = 0.5f,
                Attribute = AttributeType.Fire,
                Description = "전투 시작 시 모든 적에게 50% 화염 피해"
            }
        };

        public static SkillBag ToSkillBag() => new SkillBag(
            normalSkill: NormalSkill,
            specialSkill: SpecialSkill,
            ultimateSkill: UltimateSkill,
            talentSkill: TalentSkill,
            techniqueSkill: TechniqueSkill
        );
    }

}
