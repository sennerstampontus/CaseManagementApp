using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementApp.Models.Entity
{
    internal class AddressEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string StreetName { get; set; } = null!;
        [Required]
        [Column(TypeName = "varchar")]
        [StringLength(5)]
        public string ZipCode { get; set; } = null!;
        [Required]
        [StringLength (50)]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Country { get; set; } = null!;

        public virtual ICollection<CustomerEntity> Customers { get; set; } = new List<CustomerEntity>();
        public virtual ICollection<AdminEntity> Admins { get; set; } = new List<AdminEntity>();

    }
}
