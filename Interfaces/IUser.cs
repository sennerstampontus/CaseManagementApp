using CaseManagementApp.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementApp.Interfaces
{
    internal interface IUser
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string Email { get; set; }
        [Required]
        public int AddressId { get; set; }
        public abstract AddressEntity Address { get; set; }
    }
}
