using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.App.Abstract
{
    public interface IConsole
    {
        string ReadLine();
        char ReadKeyChar();
    }
}
