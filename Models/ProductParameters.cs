namespace educational_practice5.Models
{
    public class ProductParameters : QueryStringParameters
    {
        public ProductParameters()
        {
            OrderBy = "Price";
            OrderType = "ASC";
            Brands = "";
            Sizes = "";
            Seasons = "";
            Search = "";
        }
        
        public string Search { get; set; }
        public string Brands { get; set; }
        public string Sizes { get; set; }
        public string Seasons { get; set; }
    }
}
