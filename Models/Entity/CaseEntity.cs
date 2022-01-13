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
        public string CaseId { get; set; } = Guid.NewGuid().ToString("N").Substring(0,12);
        
        [Required]
        [StringLength(50)]
        public string Subject { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string State { get; set; } = null!;

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        [Required]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public virtual AdminEntity? AdminEntity { get; set; }

    }
}
