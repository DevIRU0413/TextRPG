namespace SpeedTextRPG.Skills
{
    using System;

    namespace SpeedTextRPG.Skills
    {
        public class SkillBag
        {
            private Skill _normal;
            private Skill _special;
            private Skill _ultimate;
            private Skill? _talent;
            private Skill? _technique;

            public SkillBag(Skill normalSkill, Skill specialSkill, Skill ultimateSkill, Skill? talentSkill = null, Skill? techniqueSkill = null)
            {
                _normal = normalSkill;
                _special = specialSkill;
                _ultimate = ultimateSkill;
                _talent = talentSkill;
                _technique = techniqueSkill;
            }

            public void UseBattleSkill(SkillType useSkill, Character user, Character target)
            {
                switch (useSkill)
                {
                    case SkillType.Normal:
                        ActionSkill(_normal, user, target);
                        break;

                    case SkillType.Special:
                        ActionSkill(_special, user, target);
                        break;

                    case SkillType.Ultimate:
                        ActionSkill(_ultimate, user, target);
                        break;

                    case SkillType.Talent:
                    case SkillType.Technique:
                        Console.WriteLine($"[{useSkill}] 스킬은 직접 사용할 수 없습니다.");
                        break;

                    default: Console.WriteLine("ERROR"); return;
                }
            }

            private void ActionSkill(Skill skill, Character user, Character target)
            {
                Console.WriteLine($"{user.Name} → {target.Name}에게 [{skill.Name}] 사용!");
                skill.Effect.Apply(user, new List<Character> { target });
            }

            public Skill? GetSkill(SkillType type)
            {
                switch (type)
                {
                    case SkillType.Normal: return _normal;
                    case SkillType.Special: return _special;
                    case SkillType.Ultimate: return _ultimate;
                    case SkillType.Talent: return _normal;
                    case SkillType.Technique: return _normal;
                    default: Console.WriteLine("ERROR"); return null;
                }
            }
        }
    }
}

