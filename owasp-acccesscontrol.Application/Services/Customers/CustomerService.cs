using System;
using owasp_acccesscontrol.Application.Interfaces;
using owasp_accesscontrol.Domain.Entities;
using owasp_accesscontrol.Domain.Interfaces;

namespace owasp_acccesscontrol.Application.Services.Customers
{
	public class CustomerService : ICustomerService
	{
        private ICustomerRepository customerRepository;

		public CustomerService(ICustomerRepository customerRepository)
		{
            this.customerRepository = customerRepository;
		}

        public int Create(Customer t)
        {
            ValidateCustomer(t);

            try
            {
              return customerRepository.Create(t);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(Customer t)
        {
            ValidateCustomer(t);

            try
            {
                return customerRepository.Delete(t);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Customer> FindAll()
        {
            return customerRepository.FindAll();
        }

        public Customer? FindById(int id)
        {
            if(id <= 0)
            {
                throw new Exception("Id cannot be less or equal to zero.");
            }

            try
            {
               return customerRepository.FindById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Customer> FindByLastname(string lastname)
        {
            if (string.IsNullOrEmpty(lastname))
            {
                throw new Exception("Lastname cannot be null or empty.");
            }

            try
            {
                return customerRepository.FindByLastname(lastname);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Customer> FindByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Name cannot be null or empty.");
            }

            try
            {
                return customerRepository.FindByLastname(name);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Update(Customer t)
        {
            try
            {
                return customerRepository.Update(t);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ValidateCustomer(Customer t)
        {
            if (t == null)
            {
                throw new Exception("Customer object can not be null.");
            }
        }
    }
}

