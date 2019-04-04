using Presentation.Interfaces;
using System;

namespace Presentation
{
    public class InputAndOutputForConsole:IInputAndOutput
    {
        public string GetUserInput()
        {
            return Console.ReadLine();
        }

        public void Write(string text)
        {
            Console.Clear();
            Console.Write(text);
        }
    }
}
