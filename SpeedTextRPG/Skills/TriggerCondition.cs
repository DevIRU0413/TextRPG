using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTextRPG.Skills
{
    public class TriggerCondition
    {
        public string ConditionType { get; set; }
        public string Parameter { get; set; }
        public bool Evaluate(Character user)
        {
            // 조건 검사 로직
            return true;
        }
    }
}
