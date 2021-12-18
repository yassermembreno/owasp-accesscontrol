using System;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using owasp_accesscontrol.Domain.Entities;
using owasp_accesscontrol.Domain.Interfaces;

namespace owasp_accesscontrol.Infrastructure.Repositories.Customers
{
	public class CustomerRepository : ICustomerRepository
	{
        private IDBLoanManagerContext dbloanmanagerContext;

        public CustomerRepository(IDBLoanManagerContext dbloanmanagerContext)
        {
            this.dbloanmanagerContext = dbloanmanagerContext;
        }

        public int Create(Customer t)
        {
            dbloanmanagerContext.Customers.Add(t);
            return dbloanmanagerContext.SaveChanges();
        }

        public bool Delete(Customer t)
        {
            dbloanmanagerContext.Customers.Remove(t);
            return dbloanmanagerContext.SaveChanges() > 0;
        }

        public IEnumerable<Customer> FindAll()
        {
            return dbloanmanagerContext.Customers.ToList();
        }

        public Customer? FindById(int id)
        {
            MySqlParameter parameter = new MySqlParameter("Id", id);
            string sql = @$"Select
                                  *
                              From Customer
                             Where CustomerId = @Id";

            return dbloanmanagerContext.Customers.FromSqlRaw(sql, parameter).FirstOrDefault();
        }

        public IEnumerable<Customer> FindByLastname(string lastname)
        {
            MySqlParameter parameter = new MySqlParameter("Lastname", lastname);
            string sql = @$"Select
                                  *
                              From Customer
                             Where Lastnames = @Lastname";

            return dbloanmanagerContext.Customers.FromSqlRaw(sql, parameter).ToList();
        }

        public IEnumerable<Customer> FindByName(string name)
        {
            MySqlParameter parameter = new MySqlParameter("Name", name);
            string sql = @$"Select
                                   *
                              From Customer
                             Where Names = @Name";

            return dbloanmanagerContext.Customers.FromSqlRaw(sql, parameter).ToList();
        }

        public int Update(Customer t)
        {
            dbloanmanagerContext.Customers.Update(t);
            return dbloanmanagerContext.SaveChanges();
        }

        
    }
}

