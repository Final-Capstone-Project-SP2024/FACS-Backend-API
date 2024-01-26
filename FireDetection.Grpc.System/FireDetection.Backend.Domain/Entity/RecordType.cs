using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireDetection.Backend.Domain.Entity
{
    public class RecordType
    {
        [Key]
        public int RecordTypeId { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Record> Records { get; set; }
    }
}
