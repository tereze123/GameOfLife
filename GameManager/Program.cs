using FileOperations;
using FileOperations.Interfaces;
using GamePlayManaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using InputAndOutput.Interfaces;
using System.IO;
using GameEngine.Interfaces;

namespace GameOfLife
{
    public class Program
    {               
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
            .AddTransient<IFileOperations, FileOperations.FileOperations>()
            .AddTransient<IInputAndOutput, InputAndOutput.InputAndOutput>()
            .AddTransient<IGameEngine, GameEngine.GameEngine>()
            .BuildServiceProvider();

            var fileOperations = serviceProvider.GetService<IFileOperations>();
            var inputAndOutput = serviceProvider.GetService<IInputAndOutput>();
            var gameEngine = serviceProvider.GetService<IGameEngine>();

            GameManager gameManager = new GameManager(fileOperations, inputAndOutput, gameEngine);

            gameManager.StartMenu();
        }
    }
}
