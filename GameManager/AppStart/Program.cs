using FileOperations.Interfaces;
using GameEngine;
using GameEngine.Interfaces;
using GamePlayManaging;
using InputAndOutput.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfLife
{
    public class Program
    {               
        static void Main(string[] args)
        {

            var serviceProvider = new ServiceCollection()
            .AddTransient<IFileOperations, FileOperations.FileOperations>()
            .AddTransient<IInputAndOutput, InputAndOutput.InputAndOutput>()
            .AddTransient<IGameEngine, GameEngine.GameEngine>()
            .BuildServiceProvider();

            var fileOperations = serviceProvider.GetService<IFileOperations>();
            var inputAndOutput = serviceProvider.GetService<IInputAndOutput>();
            var gameEngine = serviceProvider.GetService<IGameEngine>();

            GameManager gameManager = new GameManager(new GameModelState(),fileOperations, inputAndOutput, gameEngine);

            gameManager.StartMenu();
        }
    }
}
