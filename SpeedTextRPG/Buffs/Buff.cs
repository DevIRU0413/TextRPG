using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTextRPG.Buffs
{
    public class Buff
    {
        public string Name { get; set; } = "기본 버프";
        public string Description { get; set; } = "";
        public StatType Stat { get; set; }
        public float Amount { get; set; }
        public int Duration { get; set; }

        public bool IsStackable { get; set; } = false;
        public int MaxStacks { get; set; } = 1;
        public int CurrentStacks { get; set; } = 1;

        public BuffType Type { get; set; } = BuffType.Buff;

        public float TotalAmount => Amount * CurrentStacks;

        public void RefreshOrStack()
        {
            if (IsStackable && CurrentStacks < MaxStacks)
            {
                CurrentStacks++;
            }
            else
            {
                Duration = Math.Max(Duration, 1);
            }
        }

        public override string ToString()
        {
            return $"{Name} ({Stat} {Amount:+#;-#} x {CurrentStacks}, {Duration}턴 남음)";
        }
    }
}
