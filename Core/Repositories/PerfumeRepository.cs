using FinekraApi.Core.Entities;
using FinekraApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinekraApi.Core.Repositories
{
    public class PerfumeRepository : IPerfumeRepository
    {
        private readonly PerfumeDbContext _dbContext; // Gerçek veritabanına erişim için DbContext

        public PerfumeRepository(PerfumeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Perfumes> GetPerfumes()
        {
            return _dbContext.Perfumes.ToList();
        }

        public Perfumes GetPerfumeById(int perfumeId)
        {
            return _dbContext.Perfumes.FirstOrDefault(p => p.PerfumeId == perfumeId);
        }

        public IEnumerable<Perfumes> SearchPerfumesByName(string perfumeName)
        {
            // perfumeName null değilse ve içeriyorsa filtreleme yap
            if (!string.IsNullOrEmpty(perfumeName))
            {
                return _dbContext.Perfumes
                    .Where(p => (p.PerfumeName ?? "").Contains(perfumeName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Eğer perfumeName null ise, tüm kayıtları döndür
            return _dbContext.Perfumes.ToList();
        }

        public IEnumerable<Perfumes> SortPerfumesByPrice()
        {
            return _dbContext.Perfumes.OrderBy(p => p.Price).ToList();
        }

        public IEnumerable<Perfumes> FilterPerfumesByBrand(string brandName)
        {
            // brandName null değilse ve içeriyorsa filtreleme yap
            if (!string.IsNullOrEmpty(brandName))
            {
                return _dbContext.Perfumes
                    .Where(p => p.Brand.BrandName.Contains(brandName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Eğer brandName null ise, tüm kayıtları döndür
            return _dbContext.Perfumes.ToList();
        }

        public IEnumerable<Perfumes> GetPagedPerfumes(int page, int pageSize)
        {
            // Sayfalama işlevselliğini ekleyebilirsiniz
            int skip = (page - 1) * pageSize;
            return _dbContext.Perfumes.Skip(skip).Take(pageSize).ToList();
        }
    }
}
