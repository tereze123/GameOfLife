using Application.AppStart;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Interfaces;
namespace Application.Factory
{
    public class GameManagerFactory
    {
        public GameManager CreateGameManager()
        {
            Service services = new Service();
            var serviceProvider = services.GetServiceProvider();
            var gameField = serviceProvider.GetService<IGameField>();
            var drawField = serviceProvider.GetService<IDrawField>();
            var inputAndOutputService = serviceProvider.GetService<IInputAndOutput>();

            return new GameManager(
                drawField,
                inputAndOutputService,
                gameField
                );
        }
    }
}
