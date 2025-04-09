using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTextRPG.Interfaces
{
    public interface ISkillEffect
    {
        string Description { get; }
        void Apply(Character user, List<Character> targets);
    }
}
