using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart_controller.Models
{
    public class User
    {
        [MaxLength(36)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Username { get; set; }

        [MaxLength(250)]
        [Required]
        public string Password { get; set; }

        //Foreign key to product model, persistant storage for purchased order
        //[MaxLength(36)]
        //public string ProductId { get; set; }

        //public virtual Product Product { get; set; }
    }
}
