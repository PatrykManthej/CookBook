using CookBook.App.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.App.Managers
{
    public class BaseManager
    {
        protected IConsole _console;

        public BaseManager(IConsole console)
        {
            _console = console;
        }

        public int IdHandling()
        {
            int id = 0;
            var enteredIdIsValid = false;
            do
            {
                var enteredId = _console.ReadLine();
                if (enteredId == "n")
                {
                    return 0;
                }
                Int32.TryParse(enteredId, out id);
                if (id > 0)
                {
                    enteredIdIsValid = true;
                }
                else
                {
                    Console.WriteLine("Entered id is invalid. Please enter valid id or enter 'n' to go back:");
                }
            }
            while (enteredIdIsValid == false);
            return id;
        }
    }
}
