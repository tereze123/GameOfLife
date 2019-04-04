using Application.Factory;

namespace Application.AppStart
{
    public class Program
    {               
        static void Main(string[] args)
        {
            GameManagerFactory factory = new GameManagerFactory();
            GameManager gameManager = factory.CreateGameManager();
            gameManager.Start();
        }
    }
}
