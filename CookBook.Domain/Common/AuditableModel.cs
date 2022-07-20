using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CookBook.Domain.Common
{
    public class AuditableModel
    {
        [JsonIgnore]
        [XmlIgnore]
        public int CreatedById { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public DateTime DateTime { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public int? ModifiedById { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        public DateTime? ModifiedDateTime { get; set; }
    }
}
