using System;

namespace GameOfLife
{
    public class ConsoleManipulations
    {
        public void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        public string GetUserInput()
        {
            return Console.ReadLine();
        }

        public void ClearGameScreen()
        {
            Console.Clear();
        }

        public void WriteToConsole(string text)
        {
            Console.Write(text);
        }

        public void WriteToConsoleOneLine(string text)
        {
            Console.WriteLine(text);
        }

        public void WriteManyLinesToConsole(params string[] text)
        {
            foreach (var item in text)
            {
                Console.WriteLine(item);
            }
        }

        private bool BackgroundBlackAndForeGroundBlack(ColorEnum backgroundColor, ColorEnum ForeGroundColor)
        {
            if (backgroundColor == ColorEnum.Black && ForeGroundColor == ColorEnum.Black)
                return true;
            else return false;
        }

        private bool BackgroundBlackAndForegroundWhite(ColorEnum backgroundColor, ColorEnum ForeGroundColor)
        {
            if (backgroundColor == ColorEnum.Black && ForeGroundColor == ColorEnum.White)
                return true;
            else return false;
        }

        private bool BackgroundWhiteAndForeGroundWhite(ColorEnum backgroundColor, ColorEnum ForeGroundColor)
        {
            if (backgroundColor == ColorEnum.White && ForeGroundColor == ColorEnum.White)
                return true;
            else return false;
        }



        public static bool EscapeWasPressed()
        {
            return (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape);
        }

    }
}
