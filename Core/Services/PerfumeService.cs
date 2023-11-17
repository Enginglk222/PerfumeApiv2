using FinekraApi.Core.Entities;
using FinekraApi.Core.Interfaces;
using System.Collections.Generic;

namespace FinekraApi.Core.Services
{
    public class PerfumeService : IPerfumeService
    {
        private readonly IPerfumeRepository _perfumeRepository;

        public PerfumeService(IPerfumeRepository perfumeRepository)
        {
            _perfumeRepository = perfumeRepository;
        }

        public IEnumerable<Perfumes> GetPerfumes()
        {
            return _perfumeRepository.GetPerfumes();
        }

        public Perfumes GetPerfumeById(int perfumeId)
        {
            return _perfumeRepository.GetPerfumeById(perfumeId);
        }

        public IEnumerable<Perfumes> SearchPerfumesByName(string perfumeName)
        {
            return _perfumeRepository.SearchPerfumesByName(perfumeName);
        }

        public IEnumerable<Perfumes> SortPerfumesByPrice()
        {
            return _perfumeRepository.SortPerfumesByPrice();
        }

        public IEnumerable<Perfumes> FilterPerfumesByBrand(string brandName)
        {
            return _perfumeRepository.FilterPerfumesByBrand(brandName);
        }

        public IEnumerable<Perfumes> GetPagedPerfumes(int page, int pageSize)
        {
            
            return _perfumeRepository.GetPagedPerfumes(page, pageSize);
        }

        public List<Perfumes> GetBrandParfumes(int brandId)
        {
            return _perfumeRepository.GetBrandByParfumes(brandId);
        }

        List<Perfumes> IPerfumeService.GetPagedPerfumes(int page, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
