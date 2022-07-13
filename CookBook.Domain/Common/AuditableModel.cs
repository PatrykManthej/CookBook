using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Domain.Common
{
    public class AuditableModel
    {
        [JsonIgnore]
        public int CreatedById { get; set; }
        [JsonIgnore]
        public DateTime DateTime { get; set; }
        [JsonIgnore]
        public int? ModifiedById { get; set; }
        [JsonIgnore]
        public DateTime? ModifiedDateTime { get; set; }
    }
}
