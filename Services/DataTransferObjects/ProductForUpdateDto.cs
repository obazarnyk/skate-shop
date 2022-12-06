using System;
using System.ComponentModel.DataAnnotations;
using educational_practice5.Models;

namespace educational_practice5.Services.DataTransferObjects
{
    public class ProductForUpdateDto
    {
        [Required(ErrorMessage = "Price is required")]
        [RequiredGreaterThanZero]
        public int Price{ get; set; }
        
        [Required(ErrorMessage = "Price is required")]
        public int NewPrice{ get; set; }
        
        [Required]
        public int Count { get; set; }
    }
}
