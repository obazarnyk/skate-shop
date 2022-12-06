using System;
using System.Collections.Generic;
using educational_practice5.Database;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using educational_practice5.Models;
using educational_practice5.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Firebase.Auth;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Threading;
using Firebase.Storage;
using System.IO;

namespace educational_practice5.Repositories.Implimentations
{
    public class ProductRepository: BaseRepository<Product>, IProductRepository
    {
        private readonly RepositoryContext _context;
        public ProductRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {
            _context = repositoryContext;
        }
        public Product GetProductById(Guid patientId) 
        { 
            return FindByCondition(patient => patient.ProductId.Equals(patientId)).FirstOrDefault();
        }
        public void CreateProduct(Product product) { Create(product); }
        public void DeleteProduct(Product product) { Delete(product); }
        public void UpdateProduct(Product product) { Update(product); }
    
        
        
        public IEnumerable<Product> GetProducts(ProductParameters productParameters)
        {
            var sql = $"select * from products where lower(\"Title\") LIKE '%{productParameters.Search.ToLower()}%'";

            if (productParameters.Brands != "") sql += $" and \"Brand\" IN ({productParameters.Brands}) ";
            if (productParameters.Sizes != "") sql += $" and \"Size\" IN ({productParameters.Sizes}) ";
            if (productParameters.Seasons != "") sql += $" and \"Season\" IN ({productParameters.Seasons}) ";

            sql += $"order by \"{productParameters.OrderBy}\" {productParameters.OrderType} ";
            sql += $"offset {productParameters.PageNumber} rows FETCH NEXT {productParameters.PageSize} ROWS ONLY;";

            return _context.products.FromSqlRaw(sql).ToList();
        }
        
        public bool productExists(Guid id)
        {
            return _context.products.Any(e => e.ProductId == id);
        }
        
        private void SearchByTitle(ref IQueryable<Product> patients, string patientName)
        {
            if (!patients.Any() || string.IsNullOrWhiteSpace(patientName))
                return;
           
            patients = patients.Where(o => o.Title.ToLower().Contains(patientName.Trim().ToLower()) );
        }

        public async Task<IEnumerable<string>> UploadImages(IFormFileCollection files,IConfiguration configuration,string contentPath)
        {
            List<string> urls = new List<string>();

            string api = configuration["FireBase:ApiKey"];
            string email = configuration["FireBase:AuthEmail"];
            string password = configuration["FireBase:AuthPassword"];
            string bucket = configuration["FireBase:Bucket"];

            var auth = new FirebaseAuthProvider(new FirebaseConfig(api));
            var signIn = await auth.SignInWithEmailAndPasswordAsync(email,password);

            var cancellationToken = new CancellationTokenSource();

            foreach (var file in files)
            {        
                var upload =  new FirebaseStorage(bucket, new FirebaseStorageOptions()
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(signIn.FirebaseToken),
                    ThrowOnCancel = true
                }).Child(file.FileName);

                using (var stream = file.OpenReadStream())
                {
                    await upload.PutAsync(stream,cancellationToken.Token);
                }
                urls.Add(await upload.GetDownloadUrlAsync());
            }
            return urls;
        }
    }
}
