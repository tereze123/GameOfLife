using InputAndOutput.Interfaces;

namespace InputAndOutput
{
    public class ValidateUserInput:IValidateUserInput
    {
        public int ParseFromStringToInt(string userInput)
        {
            return int.Parse(userInput);
        }

        public bool ValidateStartMenuUserInput(string userInput)
        {
           // System.Enum.TryParse
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
