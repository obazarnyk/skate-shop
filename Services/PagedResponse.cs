using System.Collections.Generic;
using System.Linq;
using educational_practice5.Services.DataTransferObjects;

namespace educational_practice5.Repositories.Implimentations
{
    public class PagedResponse
    {
        public PagedResponse(IEnumerable<ProductDto> data, int amount)
        {
            Data = data;
            Count = amount;
        }
        public int Count { get; set; }
        public IEnumerable<ProductDto> Data { get; set; }
    }
}