using System;
namespace owasp_accesscontrol.Domain.Entities
{
	public class AppSetting
	{
		public string? SecretKey { get; set; }
		public string? Issuer { get; set; }
		public string? Audience { get; set; }
	}
}

