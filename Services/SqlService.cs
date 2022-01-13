using CaseManagementApp.Data;
using CaseManagementApp.Models;
using CaseManagementApp.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseManagementApp.Services
{
    internal class SqlService
    {
        private SqlContext _context;

        public  SqlService()
        {
            _context = new SqlContext();
        }


        #region CREATE
        public int CreateAddress(Address address)
        {
            var _address = _context.Addresses.Where(x => x.StreetName == address.StreetName && x.ZipCode == address.ZipCode).FirstOrDefault();
            if (_address == null)
            {
                var addressEntity = new AddressEntity() { StreetName = address.StreetName, ZipCode = address.ZipCode, City = address.City, Country = address.Country};
                _context.Addresses.Add(addressEntity);
                _context.SaveChanges();
                return addressEntity.Id;
            }

            return _address.Id;
        }

       
        public int CreateCustomer(Customer customer)
        {
            var _customer = _context.Customers.Where(x => x.Email == customer.Email).FirstOrDefault();
            if (_customer == null)
            {
                var customerEntity = new CustomerEntity();

                customerEntity.FirstName = customer.FirstName;
                customerEntity.LastName = customer.LastName;
                customerEntity.Email = customer.Email;
                customerEntity.PhoneNumber = customer.PhoneNumber;
                customerEntity.AddressId = CreateAddress(customer.Address);
                
                _context.Customers.Add(customerEntity);
                _context.SaveChanges();

                return customerEntity.Id;
            }

            return _customer.Id;

        }

        public int CreateAdmin(Admin admin)
        {
            var _admin = _context.Admins.Where(x => x.Email == admin.Email).FirstOrDefault();
            if(_admin == null)
            {
                var adminEntity = new AdminEntity();

                adminEntity.FirstName = admin.FirstName;
                adminEntity.LastName = admin.LastName;
                adminEntity.Email = admin.Email;
                adminEntity.AddressId = CreateAddress(admin.Address);
                

                _context.Admins.Add(adminEntity);
                _context.SaveChanges();

                return adminEntity.Id;
            }
            return _admin.Id;
        }

        public string CreateCase(Case _case)
        {
            var caseEntity = new CaseEntity();

            caseEntity.Subject = _case.Subject;
            caseEntity.Description = _case.Description;
            caseEntity.State = _case.State.ToString();
           

            _context.Cases.Add(caseEntity);
            _context.SaveChanges();


            return caseEntity.CaseId;
        }

        #endregion

        #region READ

        //Single result

        public AddressEntity GetAddress(int id)
        {
            return _context.Addresses.SingleOrDefault(x => x.Id == id);
        }

        public CustomerEntity GetCustomer(int id)
        {
            return _context.Customers.Include(x => x.Address).SingleOrDefault(x =>x.Id == id);
        }

        public AdminEntity GetAmin(int id)
        {
            return _context.Admins.Include(x => x.Address).SingleOrDefault(x => x.Id == id);
        }


        public CaseEntity GetCase(int id)
        {
            return _context.Cases.SingleOrDefault(x => x.CaseId == id.ToString());
        }

        //Multiple results

        public IEnumerable<AddressEntity> GetAddresses()
        {
            return _context.Addresses;
        }

        public IEnumerable<CustomerEntity> GetCustomers()
        {
            return _context.Customers;
        }

        public IEnumerable<AdminEntity> GetAdmins()
        {
            return _context.Admins;
        }
        
        public IEnumerable<CaseEntity> GetCases()
        {
            return _context.Cases;
        }


        #endregion

    }

}

