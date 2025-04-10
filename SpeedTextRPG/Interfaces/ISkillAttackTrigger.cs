using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTextRPG.Interfaces
{
    public interface ISkillAttackTrigger
    {
        void OnAttack(Character user, Character target);
    }
}
