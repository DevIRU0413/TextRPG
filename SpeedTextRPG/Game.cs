using SpeedTextRPG.Managers;

namespace SpeedTextRPG
{
    public class Game
    {
        private bool isGameOver = false;

        public void Play()
        {
            while (!isGameOver)
            {
                Render();
                HandleInput();
                Update();
            }
        }

        private void Render()
        {
            Console.Clear();
            SceneManager.Instance.RenderCurrentScene();
        }

        private void HandleInput()
        {
            var inputKey = Console.ReadKey(true).Key;
            SceneManager.Instance.HandleInput(inputKey);
        }

        private void Update()
        {
            SceneManager.Instance.UpdateCurrentScene();
        }
    }
}