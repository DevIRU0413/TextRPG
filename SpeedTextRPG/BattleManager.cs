namespace SpeedTextRPG
{
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

            // BubbleSort();
            // SelectionSort();
            InsertSort();

            _isBattle = true;
        }

        // 버블 정렬
        private void BubbleSort()
        {
            for (int i = 0; i < _characterList.Count; i++)
            {
                for (int j = 1; j < _characterList.Count - i; j++)
                {
                    if (_characterList[j - 1].ActionPoint > _characterList[j].ActionPoint)
                    {
                        Character temp = _characterList[j -1];
                        _characterList[j - 1] = _characterList[j];
                        _characterList[j] = temp;
                    }
                }
            }
        }
        // 선택 정렬
        private void SelectionSort()
        {
            int index = 0;
            for (int i = 0; i < _characterList.Count; i++)
            {
                index = i;
                for (int j = i + 1; j < _characterList.Count; j++)
                {
                    if (_characterList[index].ActionPoint > _characterList[j].ActionPoint)
                        index = j;
                }

                Character temp = _characterList[i];
                _characterList[i] = _characterList[index];
                _characterList[index] = temp;
            }
        }
        // 삽입 정렬
        private void InsertSort()
        {
            int j;
            for (int i = 1; i < _characterList.Count; i++)
            {
                Character pick = _characterList[i];
                for (j = i - 1; j >= 0; j--)
                {
                    // 삽입해줄 것이 현재
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
            int idx = 1;

            // 현재 턴인 녀셕이 없을 때, 첫번째 녀석 사용
            if (_curTurn == null)
                idx = 0;
            else
            {
                _curTurn.ActionPoint += GetBattleActionPoint(_curTurn);
                _curTurn.TickBuffs();
            }

            // 다음 녀석 턴 녀석 지정
            _curTurn = _characterList[idx];
            minusPoint = _curTurn.ActionPoint;
            _curTurn.turnCount++;
            // 다음턴인 녀석의 액션 포인트 만큼 전부 차감
            for (idx = 0; idx < _characterList.Count; idx++)
                _characterList[idx].ActionPoint -= minusPoint;

            // 정렬
            InsertSort();
        }


        private float GetBattleActionPoint(Character character)
        {
            return ACTIONPOINT_MAX_VALUE / character.BaseSpeed;
        }

        private void BattleChactersSetting(List<Character> list)
        {
            foreach (Character character in list)
            {
                if (character.HealthPoint <= 0)
                {
                    list.Remove(character);
                    continue;
                }

                character.ActionPoint = GetBattleActionPoint(character);
            }
        }

        public List<Character> GetAllEnemies(Character character)
        {
            if (_groupA.Contains(character))
                return _groupB;
            else if (_groupB.Contains(character))
                    return _groupA;
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


