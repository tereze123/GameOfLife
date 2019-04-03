namespace Presentation.Interfaces
{
    public interface IValidateUserInput
    {
        int ParseFromStringToInt(string userInput);
        bool ValdiatePausedGameUserInput(string userInput);
        bool ValidateFieldSizeUserInput(string userInputFieldSize);
        bool ValidateStartMenuUserInput(string userInput);
    }
}