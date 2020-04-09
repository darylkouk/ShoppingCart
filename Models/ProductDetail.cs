using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class ProductDetail
    {
        [MaxLength(36)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        //Foreign key to product model
        [MaxLength(36)]
        [Required]
        public string ProductId { get; set; }

        [MaxLength(36)]
        [Required]
        public string UserId { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }

        public int Rating { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
