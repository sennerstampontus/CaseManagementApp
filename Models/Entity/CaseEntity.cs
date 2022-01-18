using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementApp.Models.Entity
{
  
    internal class CaseEntity
    {
        [Key]
        public int CaseId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Subject { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        [Required]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;


        [Required]
        public string State { get; set; } = null!;

        public virtual CustomerEntity Customer { get; set; }
        public virtual AdminEntity Admin { get; set; }

    }
}
