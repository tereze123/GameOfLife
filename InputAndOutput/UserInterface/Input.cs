using Presentation.Interfaces;
using System;

namespace Presentation
{
    public class Input:IInput
    {
        private readonly IOutputText _outputText;
        private readonly IValidateUserInput _validateUserInput;

        public Input(IOutputText outputText, IValidateUserInput validateUserInput)
        {
            _outputText = outputText;
            _validateUserInput = validateUserInput;
        }

        public int GetValidFieldSizeFromUser()
        {
            string userInput = "";
            do
            {
                Console.Clear();
                _outputText.TextOutputForFieldSizeInput();
                userInput = Console.ReadLine();
            } while (!(_validateUserInput.ValidateFieldSizeUserInput(userInput)));
            return _validateUserInput.ParseFromStringToInt(userInput);
        }

        public int GetValidUserInputForPausedGame(int[,] firstArray)
        {
            string userInput = "";
            do
            {
                Console.Clear();
                _outputText.TextOutputForPausedGame();
                userInput = Console.ReadLine();
            } while (!(_validateUserInput.ValdiatePausedGameUserInput(userInput)));
            return _validateUserInput.ParseFromStringToInt(userInput);
        }

        public int GetValidUserInputForStartMenu()
        {
            string userInput = "";
            do
            {
                Console.Clear();
                _outputText.TextOutputForStartMenuInput();
                userInput = Console.ReadLine();
            } while (!(_validateUserInput.ValidateStartMenuUserInput(userInput)));
            return _validateUserInput.ParseFromStringToInt(userInput);
        }
    }
}
