using System.ComponentModel.DataAnnotations;

namespace FinekraApi.Core.Entities
{
    public class UserDetails
    {
        [Key]
        public int UserDetailId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
    }
}
