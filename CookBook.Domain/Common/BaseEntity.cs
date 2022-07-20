using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CookBook.Domain.Common
{
    public class BaseEntity : AuditableModel
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlElement]
        public string Name { get; set; }
    }
}
