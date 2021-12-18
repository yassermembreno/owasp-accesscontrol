using System;
using owasp_accesscontrol.Domain.Entities;

namespace owasp_acccesscontrol.Application.Interfaces
{
	public interface IUserService : IServices<User>
	{
		string Login(string userName, string password);
		bool IsTokenValid(string token);
	}
}

