using Application.Enums;

namespace Application
{
    public class ValidateUserInput: Interfaces.IValidateUserInput
    {
        public bool IsStartMenuUserInputValid(string userInput)
        {
            if (!int.TryParse(userInput, out int temp)) return false;
            var input = (StartMenuEnum)(int.Parse(userInput));
            switch (input)
            {
                case StartMenuEnum.StartNewGame:
                case StartMenuEnum.StartGameFromLoadedFile:
                case StartMenuEnum.StartMultipleGames:
                    return true;
                default: return false;
            }
        }

        public bool IsPausedGameUserInputValid(string userInput)
        {
            if (!int.TryParse(userInput, out int temp)) return false;
            var userChoice = (PausedGameMenuEnum)(int.Parse(userInput));

            switch (userChoice)
            {
                case PausedGameMenuEnum.ContinueGame:
                case PausedGameMenuEnum.SaveGame:
                case PausedGameMenuEnum.ExitTheGame:
                    return true;
                default: return false;
            }
        }

        public bool IsFieldSizeUserInputValid(string userInputFieldSize)
        {
            return ((int.TryParse(userInputFieldSize, out int temp))) ? this.IsFieldSizeLessThan50MoreOrEqualTo10(int.Parse(userInputFieldSize)) : false;
        }

        private bool IsFieldSizeLessThan50MoreOrEqualTo10(int userInputFieldSize)
        {
            return (userInputFieldSize < 50 && userInputFieldSize >= 10) ? true : false;
        }
    }
}
