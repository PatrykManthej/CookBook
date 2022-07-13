using CookBook.App.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.App.Concrete
{
    public class ConsoleWrapper : IConsole
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
        public char ReadKeyChar()
        {
            return Console.ReadKey().KeyChar;
        }
    }
}
