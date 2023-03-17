using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CustomerService
    {
        private HotelManagementContext db = new HotelManagementContext();

        public IEnumerable<Customer> GetCustomer()
        {
            IEnumerable<Customer> clist;
            try
            {
                clist = db.Customers.ToList();
                return clist;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Customer GetCustomerById(int Id)
        {
            Customer? cust;
            try
            {
                cust = db.Customers.Find(Id);
                if (cust != null)
                {
                    return cust;
                }
                else
                {
                    throw new Exception("Record not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<int> AddCustomer(Customer cust)
        {
            var Cust = new Customer()
            {
                CustomerId = cust.CustomerId,
                CustomerName = cust.CustomerName,
                CustomerDob = cust.CustomerDob,
                CustomerAddress = cust.CustomerAddress,
                CustomerContact = cust.CustomerContact,
                CustomerEmail = cust.CustomerEmail,
                HotelId = cust.HotelId,
                Age = cust.Age
            };
            db.Customers.Add(Cust);
            await db.SaveChangesAsync();
            return (int)Cust.CustomerId;
        }
        public async Task RemoveCustomer(int CustomerId)
        {
            Customer cst = db.Customers.Where((x) => x.CustomerId == CustomerId).FirstOrDefault();
            db.Customers.Remove(cst);
            await db.SaveChangesAsync();
        }
    }
}
