using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using FinekraApi.Core.Entities;
using FinekraApi.Core.Services;
using FinekraApi.Core.Interfaces;
using Microsoft.AspNet.OData.Routing;
using System;
using System.Linq;

[Produces("application/json")]
[ODataRoutePrefix("Perfumes")]
public class PerfumesController : ODataController
{
    private readonly IPerfumeService _perfumeService;

    public PerfumesController(IPerfumeService perfumeService)
    {
        _perfumeService = perfumeService;
    }

    [HttpGet]
    [EnableQuery(PageSize = 10, AllowedLogicalOperators = AllowedLogicalOperators.All)]
    [ODataRoute("")]
    public IQueryable<Perfumes> Get()
    {
        return _perfumeService.GetPerfumes().AsQueryable();
    }

    [HttpGet]
    [EnableQuery(PageSize = 10, AllowedLogicalOperators = AllowedLogicalOperators.All)]
    [ODataRoute("brands")]
    public List<Perfumes> GetBrandsParfumes(int brandId)
    {
        return _perfumeService.GetBrandParfumes(brandId);
    }


    [HttpGet]
    [EnableQuery]
    [ODataRoute("Filter")]
    public IQueryable<Perfumes> FilterPerfumesByBrand(
        [FromODataUri] string BrandName = null,
        [FromODataUri] string PerfumeName = null,
        [FromODataUri] int? minPrice = null,
        [FromODataUri] int? maxPrice = null)
    {
        IQueryable<Perfumes> perfumes = _perfumeService.GetPerfumes().AsQueryable();

        if (string.IsNullOrEmpty(BrandName))
        {
            Console.WriteLine($"BrandId filter applied: {BrandName}");
            perfumes = perfumes.Where(p => p.Brand.BrandName != null && p.Brand.BrandName.Contains(BrandName, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(PerfumeName))
        {
            perfumes = perfumes.Where(p => p.PerfumeName != null && p.PerfumeName.Contains(PerfumeName, StringComparison.OrdinalIgnoreCase));
        }

        if (minPrice.HasValue)
        {
            perfumes = perfumes.Where(p => p.Price >= minPrice);
        }

        if (maxPrice.HasValue)
        {
            perfumes = perfumes.Where(p => p.Price <= maxPrice);
        }

        return perfumes;
    }
}
