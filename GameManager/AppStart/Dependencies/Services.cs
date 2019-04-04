using FileOperations.Interfaces;
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Interfaces;

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
        .AddTransient<IDrawField, Presentation.DrawFieldForConsole>()
        .AddTransient<IValidateUserInput, Application.ValidateUserInput>()
        .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
