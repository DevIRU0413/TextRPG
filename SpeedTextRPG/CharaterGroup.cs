using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTextRPG
{
    public class CharacterGroup
    {
        private List<Character> _characters;

        public CharacterGroup(List<Character> characters)
        {
            _characters = characters;
            if (_characters == null)
                _characters = new List<Character>();
        }

        public void SetCharacter(Character character)
        {
            if (_characters.Contains(character)) return;
            _characters.Add(character);
        }
        public void RemoveCharacter(Character character)
        {
            if (!_characters.Contains(character)) return;
            _characters.Remove(character);
        }

        // 배틀 매니저에서 배틀 시작 시 사용
        public List<Character> Encounter()
        {
            foreach (Character character in _characters)
            {
                if (character == null) continue;
                character.BattleStartSetting();
            }

            return _characters;
        }
    }
}