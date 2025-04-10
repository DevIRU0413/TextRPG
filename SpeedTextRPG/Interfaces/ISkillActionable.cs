namespace SpeedTextRPG.Interfaces
{
    internal interface ISkillActionable
    {
        public void Apply(Character user, List<Character> targets);
    }
}
