using FileOperations.Interfaces;
using Domain.Interfaces;
using Application.AppStart;
using Application;
using Presentation.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace Application.Factory
{
    public class GameManagerFactory
    {
        public static GameManager CreateGameManager()
        {
            Service services = new Service();
            var serviceProvider = services.GetServiceProvider();

            var fileOperations = serviceProvider.GetService<IFileOperations>();
            var gameField = serviceProvider.GetService<IGameField>();
            var gameLogic = serviceProvider.GetService<IGameLogic>();
            var statistics = serviceProvider.GetService<IStatistics>();
            var colorOfOutput = serviceProvider.GetService<IColorOfOutput>();
            var drawField = serviceProvider.GetService<IDrawField>();
            var inputservice = serviceProvider.GetService<IInput>();
            var outputText = serviceProvider.GetService<IOutputText>();
            var validateUserInput = serviceProvider.GetService<IValidateUserInput>();

            return new GameManager(
                fileOperations,
                colorOfOutput,
                drawField,
                inputservice,
                outputText,
                validateUserInput,
                gameField,
                gameLogic,
                statistics);
        }
    }
}
