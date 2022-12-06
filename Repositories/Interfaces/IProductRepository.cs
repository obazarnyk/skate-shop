using System;

using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using educational_practice5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace educational_practice5.Repositories.Interfaces
{
    public interface IProductRepository: IBaseRepository<Product>
    {
        IEnumerable<Product> GetProducts(ProductParameters productParameters);
        // IEnumerable<Product> GetProducts(ProductParameters productParameters, string[] brands, string[] sizes, string[] seasons);
        Product GetProductById(Guid patientId);
        void CreateProduct(Product product);
        
        void DeleteProduct(Product owner);
        void UpdateProduct(Product owner);

        Task<IEnumerable<string>> UploadImages(IFormFileCollection files,IConfiguration configuration,string path);
        bool productExists(Guid id);
    }
    
}
