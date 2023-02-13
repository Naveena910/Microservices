using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    [Table("Payment")]
    public class Payment : CommonModel
    {
        [ForeignKey("UserId")]

        public User User { get; set; }

        public Guid UserId { get; set; }
        public string Type { get; set; }= string.Empty;
        public string Expiry { get; set; }
        public string? AccountNumber { get; set;}
    }
}
