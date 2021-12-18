using System;
using owasp_accesscontrol.Domain.Entities;

namespace owasp_accesscontrol.Domain.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		User? GetUser(string userName, string password);

	}
}

