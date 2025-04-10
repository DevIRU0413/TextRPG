namespace SpeedTextRPG
{
    public class CharacterGroup
    {
        private string _groupName;
        public List<Character> Characters;

        public CharacterGroup(string name)
        {
            _groupName = name;
            Characters = new();
        }
        public CharacterGroup(string name, List<Character> characters)
        {
            _groupName = name;
            Characters = characters;
            if (Characters == null)
                Characters = new List<Character>();
        }

        public void SetCharacter(Character character)
        {
            if (Characters.Contains(character)) return;
            Characters.Add(character);
        }
        public void RemoveCharacter(Character character)
        {
            if (!Characters.Contains(character)) return;
            Characters.Remove(character);
        }
    }
}