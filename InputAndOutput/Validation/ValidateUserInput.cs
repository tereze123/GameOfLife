
using Application.Enums;
using Presentation.Interfaces;

namespace Presentation
{
    public class ValidateUserInput:IValidateUserInput
    {
        public int ParseFromStringToInt(string userInput)
        {
            return int.Parse(userInput);
        }

        public bool ValidateStartMenuUserInput(string userInput)
        {
            if (!CanParseToInt(userInput)) return false;
            var input = (StartMenuEnum)(ParseFromStringToInt(userInput));
            switch (input)
            {
                case StartMenuEnum.StartNewGame:
                case StartMenuEnum.StartGameFromLoadedFile:
                case StartMenuEnum.StartMultipleGames:
                    return true;
                default: return false;
            }
        }

        public bool ValdiatePausedGameUserInput(string userInput)
        {
            if (!CanParseToInt(userInput)) return false;
            var userChoice = (PausedGameMenuEnum)(ParseFromStringToInt(userInput));

            switch (userChoice)
            {
                case PausedGameMenuEnum.ContinueGame:
                case PausedGameMenuEnum.SaveGame:
                case PausedGameMenuEnum.ExitTheGame:
                    return true;
                default: return false;
            }
        }

        public bool ValidateFieldSizeUserInput(string userInputFieldSize)
        {
            return ((CanParseToInt(userInputFieldSize))) ? ValidateFieldSizeLessThan50MoreOrEqualTo10(int.Parse(userInputFieldSize)) : false;
        }

        private bool CanParseToInt(string userInput)
        {
            return int.TryParse(userInput, out int temp);
        }

        private bool ValidateFieldSizeLessThan50MoreOrEqualTo10(int userInputFieldSize)
        {
            return (userInputFieldSize < 50 && userInputFieldSize >= 10) ? true : false;
        }
    }
}
