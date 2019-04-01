namespace GameOfLife
{
    public class UserInputValidate
    {
        public int ParseFromStringToInt(string userInput)
        {
            return int.Parse(userInput);
        }

        private bool CanParseToInt(string userInput)
        {
            int temp;
            return int.TryParse(userInput,out temp);
        }

        public bool ValidateStartMenuUserInput(string userInput)
        {
            switch (userInput)
            {
                case "1":
                case "2":
                case "3":
                    return true;
                default: return false;
            }
        }

        public bool ValdiatePausedGameUserInput(string userInput)
        {
            switch (userInput)
            {
                case "1":
                case "2":
                case "3":
                    return true;
                default: return false;
            }
        }

        public bool ValidateFieldSizeUserInput(string userInputFieldSize)
        {
            return ((CanParseToInt(userInputFieldSize))) ? ValidateFieldSizeLessThan50MoreOrEqualTo10(int.Parse(userInputFieldSize)) : false;
        }

        private bool ValidateFieldSizeLessThan50MoreOrEqualTo10(int userInputFieldSize)
        {
            return (userInputFieldSize < 50 && userInputFieldSize >= 10) ? true : false;
        }
    }
}
