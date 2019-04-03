using FileOperations.Interfaces;
using Domain.Interfaces;
using Presentation.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application.AppStart
{
    public class Service
    {
        public ServiceProvider GetServiceProvider()
        {
        var serviceProvider = new ServiceCollection()
        .AddTransient<IFileOperations, FileOperations.FileOperations>()
        .AddTransient<IGameField, Domain.GameField>()
        .AddTransient<IGameLogic, Domain.GameLogic>()
        .AddTransient<IStatistics, Domain.Statistics.Statistics>()
        .AddTransient<IColorOfOutput,Presentation.ColorOfOutput>()
        .AddTransient<IDrawField, Presentation.DrawField>()
        .AddTransient<IInput, Presentation.Input>()
        .AddTransient<IOutputText,Presentation.OutputText>()
        .AddTransient<IValidateUserInput, Presentation.ValidateUserInput>()
        .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
