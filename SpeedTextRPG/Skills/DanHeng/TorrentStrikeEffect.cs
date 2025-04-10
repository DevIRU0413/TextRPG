using SpeedTextRPG.Buffs;
using SpeedTextRPG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTextRPG.Skills.DanHeng
{
    public class TorrentStrikeEffect : SkillEffect, ISkillActionable
    {
        public float PowerRatio { get; set; } = 1.3f;
        public float SpdReductionPercent = 0.12f;
        public int DebuffDuration = 2;
        public void Apply(Character user, List<Character> targets)
        {
            var target = targets[0];
            target.ReceiveDamage(new(user, target, Attribute, PowerRatio, 0));
            Console.WriteLine($"{target.Name}에게 바람 피해! (운기 창술•질우)");

            bool isCritical = new Random().NextDouble() < 0.5; // 치명타 확률은 임시 50%
            if (isCritical)
            {
                Buff debuff = new Buff
                {
                    Name = "SPD 감소",
                    Description = $"SPD -{SpdReductionPercent * 100}%",
                    Stat = StatType.SPD,
                    Amount = -target.BaseSpeed * SpdReductionPercent,
                    Duration = DebuffDuration,
                    Type = BuffType.Debuff,
                    IsStackable = false
                };
                target.ApplyBuff(debuff);
            }
        }
    }
}
