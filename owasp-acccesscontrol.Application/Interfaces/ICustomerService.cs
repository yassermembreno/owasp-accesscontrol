using System;
using owasp_accesscontrol.Domain.Entities;

namespace owasp_acccesscontrol.Application.Interfaces
{
	public interface ICustomerService : IServices<Customer>
	{
		Customer? FindById(int id);
		IEnumerable<Customer> FindByLastname(string lastname);
		IEnumerable<Customer> FindByName(string name);
	}
}

