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
        private bool _isTargetSelectionMode = false;

        private List<Skill> _selectableSkills = new();
        private Skill _selectedSkill = null;
        private List<Character> _targetCandidates = new();




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
                new Character("Goblin", AttributeType.Wind, 3, 8000, 8000, 240, 100, 100, enemySkillBag),
                new Character("Orc", AttributeType.Physical, 3, 11000, 11000, 260, 150, 90, enemySkillBag),
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

            if (_isTargetSelectionMode)
            {
                Console.WriteLine($"\n🎯 대상 선택: {_selectedSkill.Name}");
                for (int i = 0; i < _targetCandidates.Count; i++)
                {
                    var c = _targetCandidates[i];
                    Console.WriteLine($"[{i + 1}] {c.Name} - HP: {c.HealthPoint}/{c.HealthMaxPoint}");
                }
                Console.WriteLine("[B] 돌아가기");
                return;
            }

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
                Console.WriteLine("[B] 돌아가기");
                return;
            }

            if (turnCharacter != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n▶ [현재 턴] {turnCharacter.Name}");
                Console.ResetColor();

                int energy = turnCharacter.SkillBag.HaveEnergyGain;
                Console.ForegroundColor = energy >= 100 ? ConsoleColor.Magenta : ConsoleColor.Gray;
                Console.WriteLine($"에너지: {BuildEnergyBar(energy, 100)}");
                Console.ResetColor();

                if (energy >= 100 && turnCharacter.SkillBag.HasSkill(SkillType.Ultimate))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("★ 궁극기 사용 가능!");
                    Console.ResetColor();
                }

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

                foreach (var buff in c.ActiveBuffs)
                {
                    Console.ForegroundColor = buff.Type == BuffType.Buff ? ConsoleColor.Green : ConsoleColor.Red;
                    string icon = buff.Type == BuffType.Buff ? "🟢" : "🔴";
                    string stackInfo = buff.IsStackable ? $" x{buff.CurrentStacks}" : "";
                    Console.WriteLine($"  {icon} {buff.Stat}+{buff.TotalAmount} ({buff.Duration}턴 남음){stackInfo}");
                }

                Console.ResetColor();
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
            if (_battleEnded)
            {
                if (key == ConsoleKey.Escape)
                    SceneManager.Instance.ChangeScene(SceneType.Title);
                return;
            }

            var turnCharacter = BattleManager.Instance.GetTurnCharacter();

            if (_isTargetSelectionMode)
            {
                if (char.IsDigit((char)key))
                {
                    int index = (int)char.GetNumericValue((char)key) - 1;
                    if (index >= 0 && index < _targetCandidates.Count)
                    {
                        var target = _targetCandidates[index];
                        turnCharacter.SkillBag.UseBattleSkill(_selectedSkill.Type, turnCharacter, new List<Character> { target });

                        BattleManager.Instance.NextTurn();
                        _isTargetSelectionMode = false;
                        _selectedSkill = null;
                        Render();
                    }
                }
                else if (key == ConsoleKey.B)
                {
                    _isTargetSelectionMode = false;
                    _isSkillSelectionMode = true;
                    _selectedSkill = null;
                    Render();
                }
                return;
            }

            if (_isSkillSelectionMode)
            {
                if (char.IsDigit((char)key))
                {
                    int index = (int)char.GetNumericValue((char)key) - 1;
                    if (index >= 0 && index < _selectableSkills.Count)
                    {
                        _selectedSkill = _selectableSkills[index];

                        _targetCandidates = _selectedSkill.Tag switch
                        {
                            SkillTag.Support => BattleManager.Instance.GetAllAllies(turnCharacter).Where(c => c.HealthPoint > 0).ToList(),
                            _ => BattleManager.Instance.GetAllEnemies(turnCharacter).Where(c => c.HealthPoint > 0).ToList()
                        };

                        if (_targetCandidates.Count == 0)
                        {
                            Console.WriteLine("유효한 대상이 없습니다.");
                            _isSkillSelectionMode = false;
                            return;
                        }

                        _isSkillSelectionMode = false;
                        _isTargetSelectionMode = true;
                        Render();
                    }
                }
                else if (key == ConsoleKey.B)
                {
                    _isSkillSelectionMode = false;
                    _selectedSkill = null;
                    Render();
                }
                return;
            }

            switch (key)
            {
                case ConsoleKey.S:
                    _isSkillSelectionMode = true;
                    Render();
                    break;
                case ConsoleKey.N:
                    Console.WriteLine($"{turnCharacter.Name} 턴을 넘깁니다...");
                    BattleManager.Instance.NextTurn();
                    Render();
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

        private string BuildEnergyBar(int current, int max, int width = 20)
        {
            int filled = (int)((float)current / max * width);
            int empty = width - filled;
            return "[" + new string('■', filled) + new string('□', empty) + $"] {current}/{max}";
        }

    }
}
