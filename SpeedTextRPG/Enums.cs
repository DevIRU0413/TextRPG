namespace SpeedTextRPG
{
    public enum GroupState
    {
        None,
        Alive, // 살아있는 상태
        Die, // 죽은 상태
    }

    public enum SkillType
    {
        Normal,    // 기본 
        Special,   // 특별 
        Ultimate,  // 궁극기
        Talent,    // 재능 (패시브)
        Technique  // 필드 기술
    }

    public enum SkillTag
    {
        Attack,
        Support,
        Debuff,
        Buff,
        Passive
    }

    public enum TargetType
    {
        Self, // 자기 자신
        
        SingleEnemy, // 단일 적
        AllEnemies, // 모든 적
        RandomEnemies, // 무작위 적

        SingleAlly, // 단일 동맹
        AllAllies, // 모든 동맹
        RandomAllies, // 무작위 동맹
    }

    public enum AttributeType
    {
        None, // Error
        Physical, // 물리
        Fire, // 불
        Ice, // 얼음
        Lightning, // 번개
        Wind, // 바람
        Quantum, // 양자
        Imaginary, // 허수
    }

    public enum StatType
    {
        ATK,
        DEF,
        SPD,
        HP,
        MaxHP,
        AP,
    }

    public enum BuffType
    {
        Buff,
        Debuff
    }

    public enum CharacterId
    {
        Asta,
        Blade,
        Kafka,
        Arlan,
        Bronya,
        DanHeng,
        March7th,
        Seele
    }


}
