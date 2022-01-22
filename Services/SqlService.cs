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
        public async Task<int> SaveDbAddresAsync(Address address)
        {
            var _address = await Task.FromResult(_context.Addresses.Where(x => x.StreetName == address.StreetName && x.ZipCode == address.ZipCode).FirstOrDefault());
            if (_address == null)
            {
                var addressEntity = new AddressEntity() { StreetName = address.StreetName, ZipCode = address.ZipCode, City = address.City, Country = address.Country};
                _context.Addresses.Add(addressEntity);
                _context.SaveChanges();
                return addressEntity.Id;
            }

            return _address.Id;
        }

       
        public async Task<int> SaveDbCustomerAsync(Customer customer)
        {
            var _customer = await Task.FromResult(_context.Customers.Where(x => x.Email == customer.Email).FirstOrDefault());       
            if (_customer == null)
            {
                var customerEntity = new CustomerEntity();

                customerEntity.FirstName = customer.FirstName;
                customerEntity.LastName = customer.LastName;
                customerEntity.Email = customer.Email;
                customerEntity.PhoneNumber = customer.PhoneNumber;
                customerEntity.AddressId = await SaveDbAddresAsync(customer.Address);
                

                _context.Customers.Add(customerEntity);
                _context.SaveChanges();

                return customerEntity.Id;
            }

            else
                return -1;

        }

        public async Task<int> SaveDbAdminAsync(Admin admin)
        {
            var _admin = await Task.FromResult(_context.Admins.Where(x => x.Email == admin.Email).FirstOrDefault());
            if (_admin == null)
            {
                var adminEntity = new AdminEntity();

                adminEntity.FirstName = admin.FirstName;
                adminEntity.LastName = admin.LastName;
                adminEntity.Email = admin.Email;
                adminEntity.AddressId = await SaveDbAddresAsync(admin.Address);


                _context.Admins.Add(adminEntity);
                _context.SaveChanges();

                return adminEntity.Id;
            }
            else
                return -1;
        }

        public async Task<int> SaveDbCaseAsync(Case _case)
        {
            var caseEntity = new CaseEntity();
            var _customer = _context.Customers.Where(x => x.Id == _case.Customer.Id).FirstOrDefault();
            var _admin = _context.Admins.Where(x => x.Id == _case.Admin.Id).FirstOrDefault();


            caseEntity.Subject = _case.Subject;
            caseEntity.Description = _case.Description;
            caseEntity.State = _case.State.ToString();
            caseEntity.Customer = _customer;
            caseEntity.Admin = _admin;

            _context.Cases.Add(caseEntity);
            _context.SaveChanges();


            return await Task.FromResult(caseEntity.CaseId);
        }

        #endregion

        #region READ

        //Single result

        public async Task<AddressEntity> GetAddress(int id)
        {
            return await Task.FromResult(_context.Addresses.SingleOrDefault(x => x.Id == id));
        }

        public async Task<CustomerEntity> GetCustomerDbAsync(int id)
        {
            return await Task.FromResult(_context.Customers.Include(x => x.Address).SingleOrDefault(x =>x.Id == id));
        }

        public async Task<AdminEntity> GetAdminDbAsync(int id)
        {
            return await Task.FromResult(_context.Admins.Include(x => x.Address).SingleOrDefault(x => x.Id == id));
        }


        public async Task<CaseEntity> GetCaseAsync(int id)
        {
            return await Task.FromResult(_context.Cases.Include(x => x.Customer).Include(x => x.Admin).SingleOrDefault(x => x.CaseId == id));
        }

        //Multiple results

        public async Task<IEnumerable<AddressEntity>> GetAddressesAsync()
        {
            return await Task.FromResult(_context.Addresses);
        }

        public async Task<IEnumerable<CustomerEntity>> GetCustomersAsync()
        {
            return await Task.FromResult(_context.Customers.Include(x => x.Address));
        }

        public async Task<IEnumerable<AdminEntity>> GetAdminsAsync()
        {
            return await Task.FromResult(_context.Admins.Include(x => x.Address));
        }
        
        public async Task<IEnumerable<CaseEntity>> GetCasesAsync()
        {
            return await Task.FromResult(_context.Cases.Include(x => x.Customer).Include(x => x.Admin));
        }


        #endregion

        #region UPDATE

        public async void UpdateAddressDbAsync(int id, AddressEntity address)
        {
            var patchedAddress = await Task.FromResult(_context.Addresses.Find(id));

            if (patchedAddress != null && patchedAddress.Id == id)
            {
                patchedAddress.StreetName = address.StreetName;
                patchedAddress.ZipCode = address.ZipCode;
                patchedAddress.City = address.City;
                patchedAddress.Country = address.Country;

                _context.Update(patchedAddress);
                _context.SaveChanges();
            }
        }


        public async void UpdateCustomerDbAsync(int id, CustomerEntity customer)
        {
            var patchedCustomer = await Task.FromResult(_context.Customers.Find(id));

            if(patchedCustomer != null && patchedCustomer.Id == id)
            {
                patchedCustomer.FirstName = customer.FirstName;
                patchedCustomer.LastName = customer.LastName;
                patchedCustomer.Email = customer.Email;
                patchedCustomer.AddressId = customer.AddressId;

                _context.Update(patchedCustomer);
                _context.SaveChanges();
            }
        }

        public async void UpdateAdminDbAsync(int id, AdminEntity admin)
        {
            var patchedAdmin = await Task.FromResult(_context.Admins.Find(id));

            if(patchedAdmin != null && patchedAdmin.Id == id)
            {
                patchedAdmin.FirstName = admin.FirstName;
                patchedAdmin.LastName = admin.LastName;
                patchedAdmin.Email = admin.Email;
                patchedAdmin.AddressId = admin.AddressId;

                _context.Update(patchedAdmin);
                _context.SaveChanges();
            }
        }

        public async Task UpdateCaseDbAsync(int id, CaseEntity newCase)
        {
            var patchedCase = await Task.FromResult(_context.Cases.Find(id));

            if(patchedCase != null && patchedCase.CaseId == id)
            {
                patchedCase.Admin = newCase.Admin;
                patchedCase.State = newCase.State;
                patchedCase.UpdatedDate = DateTime.Now;

                _context.Update(patchedCase);
                _context.SaveChanges();
            }
        }
        #endregion
    }

}

