using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Domain.Common
{
    public class BaseEntity : AuditableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
