using System;
using System.Collections.Generic;
using System.Text;
using CookBook.Domain.Common;

namespace CookBook.Domain.Entity
{
    public class MenuAction : BaseEntity
    {
        public string MenuName { get; set; }
        public MenuAction(int id, string name, string menuName)
        {
            Id = id;
            Name = name;
            MenuName = menuName;
        }
    }
}
