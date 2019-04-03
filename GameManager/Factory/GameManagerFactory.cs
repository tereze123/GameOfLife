using FileOperations.Interfaces;
using GameEngine;
using GameEngine.Interfaces;
using GamePlayManager.AppStart;
using GamePlayManaging;
using InputAndOutput.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace GamePlayManager.Factory
{
    public class GameManagerFactory
    {
        public static GameManager CreateGameManager()
        {
            Service services = new Service();
            var serviceProvider = services.GetServiceProvider();

            var fileOperations = serviceProvider.GetService<IFileOperations>();
            var inputAndOutput = serviceProvider.GetService<IInputAndOutput>();
            var gameEngine = serviceProvider.GetService<IGameEngine>();

            return new GameManager(fileOperations, inputAndOutput, gameEngine);
        }
    }
}
