using System;
using educational_practice5.Models;

namespace educational_practice5.Services.DataTransferObjects
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string SrcFirst{ get; set; }
        public string SrcSecond{ get; set; }
        public string Title{ get; set; }
        public string FullSize { get; set; }
        public string Brand { get; set; }
        public double Size { get; set; }
        public string Color { get; set; }
        public string Season { get; set; }
        public int Price{ get; set; }
        public int NewPrice{ get; set; }
        public int Rating{ get; set; }
        public int Count { get; set; }
    }
}