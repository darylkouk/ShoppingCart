using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class PurchaseDetails
    {
        [MaxLength(36)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string ActivationCode { get; set; }

        [MaxLength(36)]
        [Required]
        public string ProductId { get; set; }

        [MaxLength(36)]
        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }

    }
}
