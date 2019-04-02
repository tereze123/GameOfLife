﻿using System;
using InputAndOutput.Interfaces;

namespace InputAndOutput
{
    public class OutputText : IOutputText
    {
        public void ClearScreen()
        {
            Console.Clear();
        }

        public void TextOutputForFieldSizeInput()
        {
            this.ClearScreen();
            Console.WriteLine("Please set the size of field (10-50)");
        }

        public void TextOutputForPausedGame()
        {
            this.ClearScreen();
            Console.WriteLine("Game Paused... Want to Continue / Save / Exit? (1/2/3)");
        }

        public void TextOutputForStartMenuInput()
        {
            Console.Clear();
            Console.WriteLine(
                "------------------WELCOME TO THE GAME OF LIFE------------------------------",
                "Please Choose Your Next Action:",
                "1 Start New Game",
                "2 Load Game From File",
                "3 Multiple games"
                );
        }
    }
}