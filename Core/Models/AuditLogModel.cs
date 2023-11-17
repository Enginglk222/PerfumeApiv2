using System;

namespace FinekraApi.Core.Models
{
    public class AuditLogModel
    {
        public string UserName { get; set; }
        public string Action { get; set; }
        public int OrderId { get; set; }
        public int PerfumeId { get; set; }
        public int OrderDetailId { get; set; }
        public DateTime Timestamp { get; set; }
        public string ErrorMessage { get; set; }

        public int AddedQuantity { get; set; }    // Sepete ekleme için
        public int RemovedQuantity { get; set; }  // Sepetten silme için
        public int UpdatedQuantity { get; set; }  // Sepet güncelleme için
    }
}
