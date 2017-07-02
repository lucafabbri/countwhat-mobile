using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using countwhat.Helpers;
using countwhat.Models;
using Newtonsoft.Json;

namespace countwhat.Services
{
	public class AccountService
	{
		protected string BaseUrl = Constants.BaseUrl;
		protected HttpClient httpclient;

		public AccountService()
		{

		}

		public async Task<bool> RefreshToken()
		{
			using (httpclient = new HttpClient())
			{
				var vars = new Dictionary<string, string>();

				vars.Add("grant_type", "refresh_token");
				vars.Add("refresh_token", Settings.RefreshToken);

				using (var content = new FormUrlEncodedContent(vars))
				{
					try
					{
						var response = await httpclient.PostAsync(new Uri(BaseUrl + "/Token", UriKind.Absolute), content);
						if (response.IsSuccessStatusCode)
						{
							var body = await response.Content.ReadAsStringAsync();
							var token = JsonConvert.DeserializeObject<TokenModel>(body);
							Settings.Token = token.access_token;
							Settings.RefreshToken = token.refresh_token;
							return true;
						}
						else { Debug.WriteLine(response.StatusCode); }
					}
					catch (HttpRequestException ex) { Debug.WriteLine(ex.Message); }
				}
			}
			return false;
		}

		public async Task<bool> Login(string username, string password)
		{
			using (httpclient = new HttpClient())
			{
				var vars = new Dictionary<string, string>();

				vars.Add("grant_type", "password");
				vars.Add("username", username);
				vars.Add("password", password);

				using (var content = new FormUrlEncodedContent(vars))
				{
					try
					{
						var response = await httpclient.PostAsync(new Uri(BaseUrl + "/Token", UriKind.Absolute), content);
						if (response.IsSuccessStatusCode)
						{
							var body = await response.Content.ReadAsStringAsync();
							var token = JsonConvert.DeserializeObject<TokenModel>(body);
							Settings.Token = token.access_token;
							Settings.RefreshToken = token.refresh_token;
							return true;
						}
						else { Debug.WriteLine(response.StatusCode); }
					}
					catch (HttpRequestException ex) { Debug.WriteLine(ex.Message); }
				}
			}
			return false;
		}
	}
}
