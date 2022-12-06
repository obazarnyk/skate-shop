using System.Collections.Generic;

namespace educational_practice5.Repositories.Interfaces
{
    public interface IDescriptionRepository
    {
        
        public IEnumerable<string> GetBrands();
        public IEnumerable<double> GetSizes();
        public IEnumerable<string> GetSeasons();
        public IEnumerable<string> GetColors();
    }
}