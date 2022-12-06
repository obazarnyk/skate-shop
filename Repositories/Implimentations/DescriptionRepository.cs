using System.Collections.Generic;
using System.Linq;
using educational_practice5.Database;
using educational_practice5.Repositories.Interfaces;

namespace educational_practice5.Repositories.Implimentations
{
    public class DescriptionRepository: IDescriptionRepository
    {
        private RepositoryContext RepositoryContext { get; set; }
        public DescriptionRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public IEnumerable<string> GetBrands()
        {
            var brands = RepositoryContext.products
                .Select(obj =>obj.Brand).Distinct().ToList();
            
            return brands;
        }

        public IEnumerable<double> GetSizes()
        {
            var sizes = RepositoryContext.products
                .Select(obj =>obj.Size).Distinct().ToList();
            
            return sizes;
        }

        public IEnumerable<string> GetSeasons()
        {
            var seasons = RepositoryContext.products
                .Select(obj =>obj.Season).Distinct().ToList();
            
            return seasons;
        }
        
        public IEnumerable<string> GetColors()
        {
            var seasons = RepositoryContext.products
                .Select(obj =>obj.Color).Distinct().ToList();
            
            return seasons;
        }
    }
}