using System.ComponentModel.DataAnnotations;

namespace FinekraApi.Core.Entities
{
    public class Brands
    {
        [Key]
        public int BrandId { get; set; }
        public string? BrandName { get; set; }
        public string? BrandDescription { get; set; }
    }
}
