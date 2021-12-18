using System;
using owasp_accesscontrol.Domain.Entities;

namespace owasp_accesscontrol.Domain.Interfaces
{
	public interface ICustomerRepository : IRepository<Customer>
	{
		Customer? FindById(int id);
		IEnumerable<Customer> FindByLastname(string lastname);
		IEnumerable<Customer> FindByName(string name);
	}
}

