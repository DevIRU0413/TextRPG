using SpeedTextRPG.Managers;
using SpeedTextRPG.Skills;
using SpeedTextRPG.Skills.Asta;

namespace SpeedTextRPG
{
    public class Game
    {
        // Text RPG Loop
        private bool _gameOver = false;
        private ConsoleKey _inputKey;

        private Player _player; // 플레이어 정보

        public void Play()
        {
            Init();
            Start();
            while (!_gameOver)
            {
                Render();
                Input();
                Update();
            }
        }
        private void Init()
        {
            Skill SpectrumBeam = new Skill
            {
                Name = "스펙트럼 빔",
                Level = 1,
                Type = SkillType.Normal,
                Tag = SkillTag.Attack,
                EnergyGain = 20,
                Cooldown = 10,
                Effect = new NormalSkillEffect
                {
                    PowerRatio = 1.10f,
                    RepeatCount = 1,
                    Target = TargetType.SingleEnemy,
                    Attribute = AttributeType.Fire,
                    Description = "적 한 명에게 아스타 공격력의 110%에 해당하는 화염 피해를 입힙니다."
                }
            };
            Skill MeteorStorm = new Skill
            {
                Name = "유성 폭풍",
                Level = 1,
                Type = SkillType.Special,
                Tag = SkillTag.Attack,
                EnergyGain = 6,
                Cooldown = 10,
                Effect = new MeteorStormEffect()
            };
            Skill AstralBlessing = new Skill
            {
                Name = "아스트랄 축복",
                Level = 1,
                Type = SkillType.Ultimate,
                Tag = SkillTag.Support,
                EnergyGain = 5,
                EnergyCost = 120,
                Cooldown = 0,
                Effect = new BuffEffect
                {
                    BuffTargetStat = "SPD",
                    BuffAmount = 36f,
                    Duration = 2,
                    IsStackable = false,
                    MaxStacks = 1,
                    Target = TargetType.AllAllies,
                    Description = "2턴 동안 모든 아군의 SPD를 36 증가시킵니다."
                }
            };
            Skill Astrometry = new Skill
            {
                Name = "천체 측정학",
                Level = 1,
                Type = SkillType.Talent,
                Tag = SkillTag.Passive,
                EnergyGain = 0,
                Cooldown = 0,
                Effect = new PassiveEffect
                {
                    Description = "공격한 적마다 충전 1중첩, 화염 약점일 경우 추가 중첩. 중첩당 아군 ATK +14%, 최대 5회. 매 턴 시작 시 중첩 3 감소."
                }
            };
            Skill MiracleFlash = new Skill
            {
                Name = "기적의 플래시",
                Level = 1, // 레벨 미지정이라 1로 설정
                Type = SkillType.Technique,
                Tag = SkillTag.Attack,
                EnergyGain = 0,
                Cooldown = 20,
                Effect = new NormalSkillEffect
                {
                    PowerRatio = 0.50f,
                    RepeatCount = 1,
                    Target = TargetType.AllEnemies,
                    Attribute = AttributeType.Fire,
                    Description = "전투 시작 시, 모든 적에게 아스타 공격력의 50% 화염 피해를 입힙니다."
                }
            };

            _player = new("DevIRU", "LOGGER");

            Character a = CharacterFactory.Instance.Create(CharacterId.DanHeng);
            Character h = CharacterFactory.Instance.Create(CharacterId.Asta);

            CharacterGroup monster = new("Test");
            _player.Group.SetCharacter(a);
            monster.SetCharacter(h);

            _player.BattleEncounter(monster);
        }
        private void Start()
        {
        }
        private void Render()
        {
            Console.Clear();
            BattleManager.Instance.Print();
        }
        private void Input()
        {
            _inputKey = ConsoleKey.None;
            _inputKey = Console.ReadKey(true).Key;
            BattleManager.Instance.NextTurn();
            _inputKey = Console.ReadKey(true).Key;
        }
        private void Update()
        {

        }
    }
}