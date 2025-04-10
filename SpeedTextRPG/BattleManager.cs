namespace SpeedTextRPG
{
    using System;
    using System.Collections.Generic;
    using SpeedTextRPG.Interfaces;

    public class BattleManager
    {
        private readonly static float ACTIONPOINT_MAX_VALUE = 10000.0f;

        private static BattleManager _battleManager;

        public static BattleManager Instance
        {
            get
            {
                if (_battleManager == null)
                    _battleManager = new BattleManager();
                return _battleManager;
            }
        }

        private List<Character> _characterList = new();
        private List<Character> _groupA;
        private List<Character> _groupB;
        private Character _curTurn;
        private bool _isBattle = false;

        public void Battle(CharacterGroup groupA, CharacterGroup groupB)
        {
            if (groupA == null || groupB == null) return;
            _groupA = groupA.Characters;
            _groupB = groupB.Characters;

            if (_groupA.Count <= 0 || _groupB.Count <= 0) return;
            _characterList.AddRange(_groupA);
            _characterList.AddRange(_groupB);

            BattleChactersSetting(_characterList);
            InsertSort();
            _isBattle = true;
        }

        private void InsertSort()
        {
            int j;
            for (int i = 1; i < _characterList.Count; i++)
            {
                Character pick = _characterList[i];
                for (j = i - 1; j >= 0; j--)
                {
                    if (_characterList[j].ActionPoint > pick.ActionPoint)
                        _characterList[j + 1] = _characterList[j];
                    else
                        break;
                }
                _characterList[j + 1] = pick;
            }
        }

        public void NextTurn()
        {
            float minusPoint = 0;
            if (_curTurn != null)
            {
                _curTurn.ActionPoint += GetBattleActionPoint(_curTurn);
                _curTurn.TickBuffs();

                foreach (var trigger in _curTurn.SkillBag.GetTurnTriggers())
                {
                    trigger.OnTurnEnd(_curTurn);
                }
            }

            InsertSort();
            _curTurn = _characterList[0];
            minusPoint = _curTurn.ActionPoint;
            _curTurn.turnCount++;

            for (int i = 0; i < _characterList.Count; i++)
            {
                _characterList[i].ActionPoint -= minusPoint;
            }

            foreach (var trigger in _curTurn.SkillBag.GetTurnTriggers())
            {
                trigger.OnTurnStart(_curTurn);
            }
        }

        private float GetBattleActionPoint(Character user)
        {
            return (int)(ACTIONPOINT_MAX_VALUE / user.GetCurrentSpeed() * 100) / 100.0f;
        }

        private void BattleChactersSetting(List<Character> list)
        {
            list.RemoveAll(c => c.HealthPoint <= 0);
            foreach (Character character in list)
            {
                character.ActionPoint = GetBattleActionPoint(character);
            }
        }

        public List<Character> GetAllEnemies(Character user)
        {
            if (_groupA.Contains(user)) return _groupB;
            else if (_groupB.Contains(user)) return _groupA;
            return new List<Character>();
        }

        public List<Character> GetAllAllies(Character user)
        {
            if (_groupA.Contains(user)) return _groupA;
            else if (_groupB.Contains(user)) return _groupB;
            return new List<Character>();
        }

        public void Print()
        {
            if (!_isBattle) return;

            if (_curTurn != null)
            {
                Console.WriteLine();
                Console.WriteLine($"MY TURN > {_curTurn.Name}");
            }

            Console.WriteLine();
            foreach (var character in _characterList)
            {
                Console.WriteLine($"[{character.Name}] [{(int)(character.ActionPoint * 100) / 100.0f}] t > {character.turnCount}");
            }
        }
    }
}
