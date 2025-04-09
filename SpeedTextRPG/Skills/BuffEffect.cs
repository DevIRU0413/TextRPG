using SpeedTextRPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTextRPG.Skills
{
    public class BuffEffect : ISkillEffect
    {
        public string BuffTargetStat { get; set; } = "ATK";
        public float BuffAmount { get; set; } = 0f;
        public int Duration { get; set; } = 1;
        public bool IsStackable { get; set; } = false;
        public int MaxStacks { get; set; } = 1;
        public TargetType Target { get; set; }
        public string Description { get; set; }

        public void Apply(Character user, List<Character> targets)
        {
            foreach (var target in targets)
            {
                Console.WriteLine($"{target.Name}의 {BuffTargetStat}이 {BuffAmount} 만큼 {Duration}턴 동안 증가합니다.");
            }
        }
    }
}
