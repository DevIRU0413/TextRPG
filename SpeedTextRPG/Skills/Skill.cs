using SpeedTextRPG.Interfaces;

namespace SpeedTextRPG.Skills
{
    public class Skill
    {
        public string Name { get; set; } // 스킬 이름
        public int Level { get; set; } // 스킬 레벨
        public SkillType Type { get; set; } // 스킬 타입
        public SkillTag Tag { get; set; } // 스킬 공격

        public int EnergyGain { get; set; } = 0;     // 기본 획득 에너지
        public int EnergyCost { get; set; } = 0;     // 궁극기용
        public int Cooldown { get; set; } = 0;       // 발동 후 재사용 대기

        public SkillEffect Effect { get; set; }
    }
}
