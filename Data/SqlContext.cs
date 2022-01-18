using CaseManagementApp.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementApp.Data
{
    internal class SqlContext : DbContext
    {
        public SqlContext()
        {

        }
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        public virtual DbSet<AddressEntity> Addresses { get; set; } = null!;
        public virtual DbSet<CaseEntity> Cases { get; set; } = null!;
        public virtual DbSet<CustomerEntity> Customers { get; set; } = null!;
        public virtual DbSet<AdminEntity> Admins { get; set; } = null!;
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Kappa\Documents\Programing\CSharp\CaseHandler\CaseManagementApp\Data\sql_CaseManager_db.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

    }
}
