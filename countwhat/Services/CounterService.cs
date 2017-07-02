using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using countwhat.Helpers;
using countwhat.Models;
using Newtonsoft.Json;

namespace countwhat.Services
{
    public class CounterService:ApiService
    {
        public CounterService()
        {
		}

		public async Task<IEnumerable<CounterDataModel>> Get(bool refreshed = false)
		{
			using (var httpclient = new HttpClient())
			{
				httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.Token);
				try
				{
					var response = await httpclient.GetAsync(new Uri(BaseUrl + "/api/Counters", UriKind.Absolute));

					if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && !refreshed)
					{
						await accountservice.RefreshToken();
						return await Get(true);
					}

					if (!response.IsSuccessStatusCode)
					{
						throw new HttpClientErrorException(response.StatusCode);
					}
					else
					{
						var body = await response.Content.ReadAsStringAsync();
						return JsonConvert.DeserializeObject<IEnumerable<CounterDataModel>>(body);
					}
				}
				catch (HttpRequestException ex)
				{
					ApiDebug("Request failed with message: " + ex.Message);
				}
			}
			return null;
		}

		public async Task<CounterDataModel> Get(string id, bool refreshed = false)
		{
			using (var httpclient = new HttpClient())
			{
				httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.Token);
				try
				{
					var response = await httpclient.GetAsync(new Uri(BaseUrl + "/api/Counters/" + id, UriKind.Absolute));

					if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && !refreshed)
					{
						await accountservice.RefreshToken();
						return await Get(id, true);
					}

					if (!response.IsSuccessStatusCode)
					{
						ApiDebug("Request is failed status code: " + response.StatusCode + " with Reason: " + response.ReasonPhrase);
					}
					else
					{
						var body = await response.Content.ReadAsStringAsync();
						return JsonConvert.DeserializeObject<CounterDataModel>(body);
					}
				}
				catch (HttpRequestException ex)
				{
					ApiDebug("Request failed with message: " + ex.Message);
				}
			}
			return null;
		}

		public async Task<CounterDataModel> Post(CounterDataModel device, bool refreshed = false)
		{
			using (var httpclient = new HttpClient())
			{
				httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.Token);
				try
				{
					var response = await httpclient.PostAsync(new Uri(BaseUrl + "/api/Counters/", UriKind.Absolute), new StringContent(JsonConvert.SerializeObject(device), Encoding.UTF8, "application/json"));

					if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && !refreshed)
					{
						await accountservice.RefreshToken();
						return await Post(device, true);
					}

					if (!response.IsSuccessStatusCode)
					{
						throw new HttpClientErrorException(response.StatusCode);
					}
					else
					{
						var body = await response.Content.ReadAsStringAsync();
						return JsonConvert.DeserializeObject<CounterDataModel>(body);
					}
				}
				catch (HttpRequestException ex)
				{
					ApiDebug("Request failed with message: " + ex.Message);
				}
			}
			return null;
		}

		public async Task<bool> Put(CounterDataModel device, bool refreshed = false)
		{
			using (var httpclient = new HttpClient())
			{
				httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.Token);
				try
				{
					var response = await httpclient.PutAsync(new Uri(BaseUrl + "/api/Counters/" + device.Id, UriKind.Absolute), new StringContent(JsonConvert.SerializeObject(device), Encoding.UTF8, "application/json"));

					if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && !refreshed)
					{
						await accountservice.RefreshToken();
						return await Put(device, true);
					}

					if (!response.IsSuccessStatusCode)
					{
						throw new HttpClientErrorException(response.StatusCode);
					}

					return response.IsSuccessStatusCode;
				}
				catch (HttpRequestException ex)
				{
					ApiDebug("Request failed with message: " + ex.Message);
				}
			}
			return false;
		}


		public async Task<bool> Delete(string id, bool refreshed = false)
		{
			using (var httpclient = new HttpClient())
			{
				httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.Token);
				try
				{
					var response = await httpclient.DeleteAsync(new Uri(BaseUrl + "/api/Counters/" + id, UriKind.Absolute));

					if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized && !refreshed)
					{
						await accountservice.RefreshToken();
						return await Delete(id, true);
					}

					if (!response.IsSuccessStatusCode && refreshed)
					{
						throw new HttpClientErrorException(response.StatusCode);
					}

					return response.IsSuccessStatusCode;
				}
				catch (HttpRequestException ex)
				{
					ApiDebug("Request failed with message: " + ex.Message);
				}
			}
			return false;
		}
    }
}
