using SpeedTextRPG.Interfaces;
using SpeedTextRPG.Managers;
using SpeedTextRPG.Skills;
using SpeedTextRPG.Skills.SpeedTextRPG.Skills;

namespace SpeedTextRPG
{
    public class BattleScene : BaseScene
    {
        private Player _player;
        private bool _battleEnded = false;

        private bool _isSkillSelectionMode = false;
        private List<Skill> _selectableSkills = new();
        private Skill _selectedSkill = null;


        public BattleScene(Player player)
        {
            _player = player;
        }

        public override void Enter()
        {
            Console.Clear();
            var enemySkillBag = new SkillBag(); // 나중에 Factory로
            CharacterGroup enemyGroup = new("Enemy Group", new List<Character>
            {
                new Character("Goblin", AttributeType.Wind, 3, 800, 800, 240, 100, 100, enemySkillBag),
                new Character("Orc", AttributeType.Physical, 3, 1100, 1100, 260, 150, 90, enemySkillBag),
            });
            _player.BattleEncounter(enemyGroup);
            Console.WriteLine("전투를 시작합니다...");
            Thread.Sleep(2000);
        }

        public override void Exit()
        {
            Console.Clear();
            Console.WriteLine("전투 종료");
            Thread.Sleep(2000);
        }

        public override void Render()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("===================== BATTLE =====================");
            Console.ResetColor();

            if (_battleEnded)
            {
                Console.WriteLine("\n🎉 전투가 종료되었습니다! [ESC]를 눌러 타이틀로 돌아가세요.");
                return;
            }

            var turnCharacter = BattleManager.Instance.GetTurnCharacter();

            if (_isSkillSelectionMode && turnCharacter != null)
            {
                Console.WriteLine($"\n▶ [현재 턴] {turnCharacter.Name} - 스킬 선택 중");
                _selectableSkills = turnCharacter.SkillBag.AllSkills
                    .Where(s => s.Type != SkillType.Talent && s.Type != SkillType.Technique)
                    .ToList();

                for (int i = 0; i < _selectableSkills.Count; i++)
                {
                    var s = _selectableSkills[i];
                    Console.ForegroundColor = s.Type switch
                    {
                        SkillType.Normal => ConsoleColor.White,
                        SkillType.Special => ConsoleColor.Cyan,
                        SkillType.Ultimate => ConsoleColor.Magenta,
                        _ => ConsoleColor.Gray
                    };

                    Console.WriteLine($"[{i + 1}] {s.Name} ({s.Type}) - {s.Effect.Description} | +{s.EnergyGain} / -{s.EnergyCost}");

                    Console.ResetColor();
                }

                Console.WriteLine("\n숫자를 입력하여 스킬을 선택하세요.");
                Console.WriteLine("[B] 돌아가기");
                return;
            }

            // 평소 턴 진행 화면
            if (turnCharacter != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n▶ [현재 턴] {turnCharacter.Name}");
                Console.ResetColor();

                Console.WriteLine("Press [S] to select skill.");
            }

            Console.WriteLine("\n[ 캐릭터 리스트 ]");
            foreach (var c in BattleManager.Instance.GetCharacterList())
            {
                var color = Console.ForegroundColor;
                if (c.HealthPoint <= 0) Console.ForegroundColor = ConsoleColor.DarkGray;
                else if (c == turnCharacter) Console.ForegroundColor = ConsoleColor.Cyan;
                else Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine($"- {c.Name} | HP: {c.HealthPoint}/{c.HealthMaxPoint} | SPD: {c.GetCurrentSpeed()} | AP: {(int)c.ActionPoint} | Buffs: {c.ActiveBuffs.Count}");

                Console.ForegroundColor = color;
            }

            Console.WriteLine("\n==================================================");
            Console.WriteLine("[S] 스킬 선택 | [N] 턴 넘기기 | [ESC] 타이틀로");
        }

        public override void Update()
        {
            if (CheckBattleEnd())
            {
                _battleEnded = true;
            }
        }

        public override void HandleInput(ConsoleKey key)
        {
            // 배틀 끝났을 시
            if (_battleEnded)
            {
                if (key == ConsoleKey.Escape)
                    SceneManager.Instance.ChangeScene(SceneType.Title);
                return;
            }

            // 현재 턴 캐릭터
            Character turnCharacter = BattleManager.Instance.GetTurnCharacter();

            // 스킬 선택 시
            if (_isSkillSelectionMode)
            {
                // 문자가 숫자인지 여부를 확인합니다. 기능
                if (char.IsDigit((char)key))
                {
                    int index = (int)char.GetNumericValue((char)key) - 1;
                    if (index >= 0 && index < _selectableSkills.Count)
                    {
                        _selectedSkill = _selectableSkills[index];

                        var targets = _selectedSkill.Tag switch
                        {
                            SkillTag.Support => BattleManager.Instance.GetAllAllies(turnCharacter),
                            _ => BattleManager.Instance.GetAllEnemies(turnCharacter)
                        };

                        if (targets.Count == 0)
                        {
                            Console.WriteLine("유효한 대상이 없습니다.");
                            _isSkillSelectionMode = false;
                            return;
                        }

                        turnCharacter.SkillBag.UseBattleSkill(_selectedSkill.Type, turnCharacter, targets);

                        BattleManager.Instance.NextTurn();
                        _isSkillSelectionMode = false;
                        _selectedSkill = null;
                    }
                }
                else if (key == ConsoleKey.B)
                {
                    Console.WriteLine("스킬 선택을 취소하고 전투 화면으로 돌아갑니다.");
                    _isSkillSelectionMode = false;
                    _selectedSkill = null;
                }


                return;
            }

            // === 평상시 ===
            switch (key)
            {
                case ConsoleKey.S:
                    _isSkillSelectionMode = true;
                    break;

                case ConsoleKey.N:
                    Console.WriteLine($"{turnCharacter.Name} 턴을 넘깁니다...");
                    BattleManager.Instance.NextTurn();
                    break;

                case ConsoleKey.Escape:
                    SceneManager.Instance.ChangeScene(SceneType.Title);
                    break;
            }
        }


        private bool CheckBattleEnd()
        {
            // 
            var aliveA = BattleManager.Instance.GetAliveAllies();
            var aliveB = BattleManager.Instance.GetAliveEnemies();

            if (aliveA.Count == 0 || aliveB.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n⚔️  전투 종료!");
                Console.WriteLine(aliveA.Count > 0 ? "승리!" : "패배...");
                Console.ResetColor();
                return true;
            }

            return false;
        }
    }
}
