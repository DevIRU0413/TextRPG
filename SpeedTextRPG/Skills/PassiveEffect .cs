using SpeedTextRPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTextRPG.Skills
{
    public class PassiveEffect : ISkillEffect
    {
        public string Description { get; set; }

        public void Apply(Character user, List<Character> targets)
        {
            Console.WriteLine("패시브 효과는 자동으로 적용되며, 전투 중 지속적으로 상태를 변경합니다.");
        }
    }
}
