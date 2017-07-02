using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using countwhat.Helpers;

namespace countwhat.Services
{
	public class ApiService
	{
		protected AccountService accountservice;
		protected string BaseUrl = Constants.BaseUrl;
		protected HttpClient httpclient;

		public ApiService()
		{
			accountservice = new AccountService();
		}

		protected void ApiDebug(string message, [CallerMemberName] string caller = null)
		{
			Debug.WriteLine("[" + this.GetType() + " - " + caller + "] " + message);
		}
	}
}
