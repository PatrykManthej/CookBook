using CookBook.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Domain.Entity
{
    public class Tag : BaseEntity
    {
        public Tag(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public Tag()
        {

        }
    }
}
