using FileOperations.Interfaces;
using GameEngine.Interfaces;
using InputAndOutput.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GamePlayManager.AppStart
{
    public class Service
    {
        public ServiceProvider GetServiceProvider()
        {
        var serviceProvider = new ServiceCollection()
        .AddTransient<IFileOperations, FileOperations.FileOperations>()
        .AddTransient<IInputAndOutput, InputAndOutput.InputAndOutput>()
        .AddTransient<IGameEngine, GameEngine.GameEngine>()
        .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
