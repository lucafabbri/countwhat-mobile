using System;
namespace countwhat.Models
{
	public class TokenModel
	{
		public string access_token { get; set; }
		public string token_type { get; set; }
		public int expires_in { get; set; }
		public string refresh_token { get; set; }
		public string userName { get; set; }
	}
}
