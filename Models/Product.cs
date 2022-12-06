
using System;
using educational_practice5.Services;
using System.ComponentModel.DataAnnotations;
namespace educational_practice5.Models
{
    
    public class Product
    {
        public Guid ProductId { get; set; }
        
        [Required(ErrorMessage = "SrcFirst is required")]
        [StringLength(300, ErrorMessage = "SrcFirst can't be longer than 60 characters")]
        public string SrcFirst{ get; set; }
        
        [Required(ErrorMessage = "SrcSecond is required")]
        [StringLength(300, ErrorMessage = "SrcSecond can't be longer than 60 characters")]
        public string SrcSecond{ get; set; }
        
        
        [Required(ErrorMessage = "Title is required")]
        [StringLength(60, ErrorMessage = "Title can't be longer than 60 characters")]
        public string Title{ get; set; }
        
        [Required(ErrorMessage = "Full size is required")]
        [StringLength(60, ErrorMessage = "Full size can't be longer than 60 characters")]
        
        
        public string FullSize { get; set; }
        [Required(ErrorMessage = "Brand is required")]
        [StringLength(60, ErrorMessage = "Brand can't be longer than 60 characters")]
        public string Brand { get; set; }
        
        [Required(ErrorMessage = "Size is required")]
        public double Size { get; set; }
        
        [Required(ErrorMessage = "Color is required")]
        [StringLength(60, ErrorMessage = "Color can't be longer than 60 characters")]
        public string Color { get; set; }
        
        [Required(ErrorMessage = "Season is required")]
        [StringLength(60, ErrorMessage = "Season can't be longer than 60 characters")]
        public string Season { get; set; }
        
        [Required(ErrorMessage = "Price is required")]
        [RequiredGreaterThanZero]
        public int Price{ get; set; }
        
        [Required(ErrorMessage = "NewPrice is required")]
        // [RequiredGreaterThanZero]
        public int NewPrice{ get; set; }
        
        [Required(ErrorMessage = "Rating is required")]
        [RequiredGreaterThanZero]
        public int Rating{ get; set; }
        
        [Required]
        public int Count { get; set; }
        
        public Order Order { get; set; }
    }
    
}