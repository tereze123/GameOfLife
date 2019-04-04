using Application.Factory;

namespace Application.AppStart
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
