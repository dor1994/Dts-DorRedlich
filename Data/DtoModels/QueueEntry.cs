using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DtoModels
{
    public class QueueEntry
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime RequestedTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User? User { get; set; }
    }
}
