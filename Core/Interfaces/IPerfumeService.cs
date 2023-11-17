using FinekraApi.Core.Entities;
using System.Collections.Generic;

namespace FinekraApi.Core.Interfaces
{
    public interface IPerfumeService
    {
        IEnumerable<Perfumes> GetPerfumes();
        Perfumes GetPerfumeById(int perfumeId);
        IEnumerable<Perfumes> SearchPerfumesByName(string perfumeName);
        IEnumerable<Perfumes> FilterPerfumesByBrand(string brandName);
        IEnumerable<Perfumes> SortPerfumesByPrice();
        IEnumerable<Perfumes> GetPagedPerfumes(int page, int pageSize);
    }
}
