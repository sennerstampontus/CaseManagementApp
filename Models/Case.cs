using CaseManagementApp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementApp.Models
{
  public enum Status
    {
        Waiting,
        Opened,
        Closed
    }
    internal class Case
    {
        public int CaseId { get; set; }
        public string Subject { get; set; } = null!;
        public string Description { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public CustomerEntity Customer { get; set; }
        public AdminEntity Admin { get; set; }
        public Status State { get; set; }

        //public List<Status> caseState = new();

    }
}
