using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTextRPG.Interfaces
{
    internal interface IAllySkillTargetedTrigger
    {
        void OnAllySkillTargeted(Character user);
    }
}
