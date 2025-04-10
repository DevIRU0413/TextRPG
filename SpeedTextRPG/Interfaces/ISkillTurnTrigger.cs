namespace SpeedTextRPG.Interfaces
{
    public interface ISkillTurnTrigger
    {
        void OnTurnStart(Character user);
        void OnTurnEnd(Character user);
    }
}
