using System;
namespace owasp_acccesscontrol.Application.Interfaces
{
	public interface IServices<T>
	{
		int Create(T t);
		int Update(T t);
		bool Delete(T t);
		IEnumerable<T> FindAll();
	}
}

