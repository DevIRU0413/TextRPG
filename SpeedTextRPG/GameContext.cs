using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTextRPG
{
    public class GameContext
    {
        public Character Caster;
        public List<Character> Targets;
        public bool TurnStarted;
        public int TurnCount;
    }
}
