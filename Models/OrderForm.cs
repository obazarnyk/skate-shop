using System;
using System.ComponentModel.DataAnnotations;

namespace educational_practice5.Models
{
    public class OrderForm
    {
        public Guid OrderFormId { get; set; }
        public Guid OrderId { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required] 
        public int Count { get; set; }
    }

    public class ProductsDto
    {
        
    }
}