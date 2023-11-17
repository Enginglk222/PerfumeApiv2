using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinekraApi.Core.Entities
{
    public class Perfumes
    {
        [Key]
        public int PerfumeId { get; set; }
        public string PerfumeName { get; set; }
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public int Price { get; set; }
        public string? PhotoPath { get; set; }

        public Brands Brand { get; set; }
    }
}
