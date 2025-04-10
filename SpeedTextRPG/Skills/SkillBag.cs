namespace SpeedTextRPG.Skills
{
    using global::SpeedTextRPG.Interfaces;
    using System;

    namespace SpeedTextRPG.Skills
    {
        public class SkillBag
        {
            private readonly int ENERGY_MAX_GAIN = 100;
            private readonly Dictionary<SkillType, Skill> _skills = new();
            public int HaveEnergyGain { get; private set; } = 0;

            // WriteLine 생각하기, params object?[]? arg
            public SkillBag(params Skill[] skills)
            {
                foreach (var skill in skills)
                {
                    _skills[skill.Type] = skill;
                }
            }
            public void UseBattleSkill(SkillType useSkill, Character user, List<Character> target)
            {
                // 실행 하려는 타입의 스킬이 있는가?
                if (!_skills.ContainsKey(useSkill))
                {
                    Console.WriteLine($"[{useSkill}] 스킬이 존재하지 않습니다.");
                    return;
                }

                // 재능 또는 필드 스킬 라면 실행 못함(버그임)
                if (useSkill == SkillType.Talent || useSkill == SkillType.Technique)
                {
                    Console.WriteLine($"[{useSkill}] 스킬은 직접 사용할 수 없습니다.");
                    return;
                }

                var skill = _skills[useSkill];
                if (skill.Effect is ISkillActionable action)
                {
                    if (skill.Type != SkillType.Ultimate || (skill.Type == SkillType.Ultimate && HaveEnergyGain >= ENERGY_MAX_GAIN))
                    {
                        action.Apply(user, target);
                        HaveEnergyGain -= skill.EnergyCost;
                        if (HaveEnergyGain < 0)
                            HaveEnergyGain = 0;
                    }
                    else
                    {
                        Console.WriteLine($"에너지가 부족하여 궁극기를 사용할 수 없습니다. (현재: {HaveEnergyGain}/{ENERGY_MAX_GAIN})");
                        return;
                    }

                    HaveEnergyGain += skill.EnergyGain;
                    if (HaveEnergyGain > ENERGY_MAX_GAIN)
                        HaveEnergyGain = ENERGY_MAX_GAIN;
                }
                else
                {
                    Console.WriteLine($"[{skill.Name}]는 실행 가능한 스킬이 아닙니다.");
                }
            }

            public Skill? GetSkill(SkillType type)
            {
                return _skills.TryGetValue(type, out var skill) ? skill : null;
            }

            public bool HasSkill(SkillType type)
            {
                return _skills.ContainsKey(type);
            }

            public IEnumerable<Skill> AllSkills => _skills.Values;

            public IEnumerable<ISkillTurnTrigger> GetTurnTriggers()
            {
                foreach (var skill in _skills.Values)
                {
                    if (skill.Effect is ISkillTurnTrigger trigger)
                        yield return trigger;
                }
            }

            public IEnumerable<ISkillAttackTrigger> GetAttackTriggers()
            {
                foreach (var skill in _skills.Values)
                {
                    if (skill.Effect is ISkillAttackTrigger trigger)
                        yield return trigger;
                }
            }
        }
    }
}

