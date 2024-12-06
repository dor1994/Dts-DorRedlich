using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DtoModels
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? FirstName { get; set; }
        public virtual ICollection<QueueEntry>? QueueEntries { get; set; }
    }
}
