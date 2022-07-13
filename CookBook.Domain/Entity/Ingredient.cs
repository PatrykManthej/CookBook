using System;
using System.Collections.Generic;
using System.Text;
using CookBook.Domain.Common;

namespace CookBook.Domain.Entity
{
    public class Ingredient : BaseEntity
    {
        public string Amount { get; set; }
        public Ingredient(string name, string amount)
        {
            Id = 1;
            Name = name;
            Amount = amount;
        }
        public Ingredient()
        {

        }
    }
}
