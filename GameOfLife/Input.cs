namespace GameOfLife
{
    public class Input
    {
        private readonly Generations _generations;
        private readonly UserInputValidate _userInputValidate;
        private readonly Output _output;
        private readonly ConsoleManipulations _console;

        public Input(Generations generations, UserInputValidate userInputValidate, Output output, ConsoleManipulations console)
        {
            this._generations = generations;
            this._userInputValidate = userInputValidate;
            this._output = output;
            this._console = console;
        }

        public int GetValidFieldSizeFromUser()
        {
            string userInput = "";
            do
            {
                this._console.ClearGameScreen();
                this._output.TextOutputForFieldSizeImput();
                userInput = this._console.GetUserInput();
            } while (!(_userInputValidate.ValidateFieldSizeUserInput(userInput)));
            return _userInputValidate.ParseFromStringToInt(userInput);
        }

        public int GetValidUserInputForPausedGame(int[,] firstArray)
        {
            string userInput = "";
            do
            {
                this._console.ClearGameScreen();
                this._output.TextOutputForPausedGame(firstArray);
                userInput = this._console.GetUserInput();
            } while (!(this._userInputValidate.ValdiatePausedGameUserInput(userInput)));
            return this._userInputValidate.ParseFromStringToInt(userInput);
        }

        public int GetValidUserInputForStartMenu()
        {
            string userInput = "";
            do
            {
                this._console.ClearGameScreen();
                this._output.TextOutputForStartMenuImput();
                userInput = _console.GetUserInput();
            } while (!(this._userInputValidate.ValidateStartMenuUserInput(userInput)));
            return this._userInputValidate.ParseFromStringToInt(userInput);
        }

        public static bool EscapeKeyWasPressed()
        {
            return ConsoleManipulations.EscapeWasPressed();
        }
    }
}
