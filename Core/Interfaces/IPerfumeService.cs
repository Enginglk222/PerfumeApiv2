using FinekraApi.Core.Entities;
using System.Collections.Generic;

namespace FinekraApi.Core.Interfaces
{
    public interface IPerfumeService
    {
        IEnumerable<Perfumes> GetPerfumes();
        Perfumes GetPerfumeById(int perfumeId);
        List<Perfumes> GetBrandParfumes(int brandId);
        IEnumerable<Perfumes> SearchPerfumesByName(string perfumeName);
        IEnumerable<Perfumes> FilterPerfumesByBrand(string brandName);
        IEnumerable<Perfumes> SortPerfumesByPrice();
        List<Perfumes> GetPagedPerfumes(int page, int pageSize);
    }
}
