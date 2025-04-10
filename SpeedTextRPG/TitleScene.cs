using SpeedTextRPG.Interfaces;
using SpeedTextRPG.Managers;

namespace SpeedTextRPG
{
    public class TitleScene : BaseScene
    {
        public override void Enter() { }
        
        public override void Exit() { }
        
        public override void Render()
        {
            string[] logo = new string[]
            {
                "+================================================================+",
                "|                                                                |",
                "| ████████╗███████╗██╗  ██╗████████╗    ██████╗ ██████╗  ██████╗ |",
                "| ╚══██╔══╝██╔════╝██║  ██║╚══██╔══╝    ██╔══██╗██╔══██╗██╔════╝ |",
                "|    ██║   █████╗    ███║     ██║       ██████╔╝██████╔╝██║  ███╗|",
                "|    ██║   ██╔══╝  ██╔══██║   ██║       ██╔══██╗██╔═══╝ ██║   ██║|",
                "|    ██║   ███████╗██║  ██║   ██║       ██║  ██║██║     ╚██████╔╝|",
                "|    ╚═╝   ╚══════╝╚═╝  ╚═╝   ╚═╝       ╚═╝  ╚═╝╚═╝      ╚═════╝ |",
                "|                                                                |",
                "+================================================================+",
                "|                                                                |",
                "|                      PRESS [ENTER] TO START                    |",
                "|                                                                |",
                "|                       PRESS [ESC] TO QUIT                      |",
                "|                                                                |",
                "+================================================================+",
            };

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var line in logo)
            {
                Console.Write("   ");
                Console.WriteLine(line);
            }
            Console.ResetColor();
        }

        public override void Update() { }

        public override void HandleInput(ConsoleKey key)
        {
            if (key == ConsoleKey.Enter)
            {
                Console.WriteLine("[TitleScene] 엔터 입력 - 게임 시작!");
                SceneManager.Instance.ChangeScene(SceneType.Battle);
            }
            else if (key == ConsoleKey.Escape)
            {
                Console.WriteLine("게임을 종료합니다.");
                Environment.Exit(0);
            }
        }
    }
}
