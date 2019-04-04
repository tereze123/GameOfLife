namespace Application.Interfaces
{
    public interface IValidateUserInput
    {
        bool IsPausedGameUserInputValid(string userInput);
        bool IsFieldSizeUserInputValid(string userInputFieldSize);
        bool IsStartMenuUserInputValid(string userInput);
    }
}