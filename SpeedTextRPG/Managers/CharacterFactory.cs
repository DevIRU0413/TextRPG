using SpeedTextRPG.Skills.Asta;
using SpeedTextRPG.Skills.Blade;
using SpeedTextRPG.Skills.DanHeng;

namespace SpeedTextRPG.Managers
{
    public class CharacterFactory
    {
        private static CharacterFactory _instance;
        public static CharacterFactory Instance => (_instance == null) ? _instance = new CharacterFactory() : _instance;

        public Character Create(CharacterId id)
        {
            switch (id)
            {
                case CharacterId.Asta:
                    return new Character("아스타", AttributeType.Fire, 1, 1023f, 1023f, 511f, 463f, 120f, AstaSkills.ToSkillBag());
                case CharacterId.DanHeng:
                    return new Character("단향", AttributeType.Wind, 1, 882f, 882f, 546f, 396f, 110f, DanHengSkills.ToSkillBag());
                /*case CharacterId.Arlan:
                    return new Character("아를란", AttributeType.Lightning, 1, 1199f, 1199f, 599f, 330f, 102f, ArlanSet.GetSkillSet());
                case CharacterId.Bronya:
                    return new Character("브로냐", AttributeType.Wind, 1, 882f, 882f, 546f, 396f, 110f, BronyaSet.GetSkillSet());
                case CharacterId.March7th:
                    return new Character("March 7th", AttributeType.Ice, 1, 1058f, 1058f, 511f, 573f, 101f, March7thSet.GetSkillSet());
                case CharacterId.Seele:
                    return new Character("제레", AttributeType.Quantum, 1, 1241f, 1241f, 582f, 533f, 99f, SeeleSet.GetSkillSet());*/
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), $"정의되지 않은 캐릭터 ID입니다: {id}");
            }
        }
    }
}