using Application.Factory;

namespace Application.AppStart
{
    public class GameStart
    {
        public static void StartGame()
        {
            GameManager gameManager = GameManagerFactory.CreateGameManager();
            gameManager.Start();
        }
    }
}
