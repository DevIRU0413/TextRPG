using SpeedTextRPG.Skills.SpeedTextRPG.Skills;
using System.Collections.Generic;

namespace SpeedTextRPG.Skills.DanHeng
{
    public static class DanHengSkills
    {
        public static Skill CloudlancerArt_NorthWind => new Skill
        {
            Name = "운기 창술•삭풍",
            Level = 1,
            Type = SkillType.Normal,
            Tag = SkillTag.Attack,
            EnergyGain = 20,
            Cooldown = 10,
            Effect = new NormalSkillEffect
            {
                Attribute = AttributeType.Wind,
                Target = TargetType.SingleEnemy,
                PowerRatio = 0.5f,
                Description = "단일 적에게 ATK 50% 바람 피해"
            }
        };
        public static Skill CloudlancerArt_Torrent => new Skill
        {
            Name = "운기 창술•질우",
            Level = 1,
            Type = SkillType.Special,
            Tag = SkillTag.Attack,
            EnergyGain = 30,
            Cooldown = 20,
            Effect = new TorrentStrikeEffect
            {
                Attribute = AttributeType.Wind,
                PowerRatio = 1.3f,
                Target = TargetType.SingleEnemy,
                Description = "ATK 130% 바람 피해 + 치명타 시 SPD 12% 감소"
            }
        };
        public static Skill EtherealDream => new Skill
        {
            Name = "동천환화, 기나긴 꿈",
            Level = 1,
            Type = SkillType.Ultimate,
            Tag = SkillTag.Attack,
            EnergyGain = 5,
            EnergyCost = 100,
            Cooldown = 30,
            Effect = new EtherealDreamEffect
            {
                Attribute = AttributeType.Wind,
                Target = TargetType.SingleEnemy,
                Description = "ATK 240% 바람 피해 + 적이 느려졌으면 72% 추가 피해",
                BasePowerRatio = 2.4f,
                BonusMultiplier = 0.72f,
            }
        };
        public static Skill SuperiorityOfReach => new Skill
        {
            Name = "각자의 장점",
            Level = 10,
            Type = SkillType.Talent,
            Tag = SkillTag.Passive,
            EnergyGain = 0,
            Cooldown = 0,
            Effect = new WindPenetrationPassiveEffect
            {
                Attribute = AttributeType.Wind,
                Target = TargetType.Self,
                Description = "아군 능력 대상 시, 다음 공격에 바람 저항 관통 +18% (2턴 재발동 제한)",
                PenetrationAmount = 0.18f,
                CooldownTurns = 2,
            }
        };
        public static Skill SplitTheSpearpoint => new Skill
        {
            Name = "섬멸의 칼날",
            Level = 1,
            Type = SkillType.Technique,
            Tag = SkillTag.Buff,
            EnergyGain = 0,
            Cooldown = 0,
            Effect = new StartOfBattleAtkBuffEffect
            {
                Attribute = AttributeType.Wind,
                Target = TargetType.Self,
                Description = "전투 시작 시 3턴 동안 ATK +40%",
                AtkBuffPercent = 0.4f,
                Duration = 3,
            }
        };

        public static SkillBag ToSkillBag() => new SkillBag(
            CloudlancerArt_NorthWind,
            CloudlancerArt_Torrent,
            EtherealDream,
            SuperiorityOfReach,
            SplitTheSpearpoint
        );
    }
}
