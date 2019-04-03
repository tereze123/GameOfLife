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
        .AddTransient<IGameField, GameEngine.GameField>()
        .AddTransient<IGameLogic, GameEngine.GameLogic>()
        .AddTransient<IStatistics, GameEngine.Statistics.Statistics>()
        .AddTransient<IColorOfOutput,InputAndOutput.ColorOfOutput>()
        .AddTransient<IDrawField, InputAndOutput.DrawField>()
        .AddTransient<IInput, InputAndOutput.Input>()
        .AddTransient<IOutputText,InputAndOutput.OutputText>()
        .AddTransient<IValidateUserInput, InputAndOutput.ValidateUserInput>()
        .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
