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
        .AddTransient<IInputAndOutput, Presentation.InputAndOutputForConsole>()
        .AddTransient<IColorOfOutput,Presentation.ColorOfOutput>()
        .AddTransient<IDrawField, Presentation.DrawField>()

        .AddTransient<IValidateUserInput, Presentation.ValidateUserInput>()
        .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
