using GamePlayManager.Factory;
using GamePlayManaging;

namespace GameOfLife
{
    public class Program
    {               
        static void Main(string[] args)
        {
            GameManager gameManager = GameManagerFactory.CreateGameManager();
            gameManager.Start();
        }
    }
}
