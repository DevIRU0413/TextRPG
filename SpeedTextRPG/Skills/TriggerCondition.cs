using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTextRPG.Skills
{
    public class TriggerCondition
    {
        public string ConditionType { get; set; }    // 예: "TurnStart", "EnemyHasWeakness"
        public string Parameter { get; set; }        // 예: "Fire", "3턴 이상"
        public bool Evaluate(Character user)
        {
            // 조건 검사 로직
            return true;
        }
    }
}
