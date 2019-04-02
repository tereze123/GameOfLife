using InputAndOutput.Enums;
using InputAndOutput.Interfaces;
using System;

namespace InputAndOutput
{
    public class ColorOfOutput : IColorOfOutput
    {
        public void SetColor(ColorEnum backgroundColor, ColorEnum foreGroundColor)
        {           
            if (backgroundColor == ColorEnum.Black)
            {
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
            if (foreGroundColor == ColorEnum.White)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }
    }
}
