using DoorManagement.Common.Interfaces;
using DoorManagement.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DoorManagement.ServiceAccessLayer
{
    [Export(typeof(IDoorManagementServiceAccess))]
    public class DoorManagementServiceAccess : IDoorManagementServiceAccess
    {
        public async Task<List<Door>> FetchDoorsAsync()
        {
            try
            {
                return await GetAsync<List<Door>>("http://localhost:55664/api/DoorManagement/FetchAllDoors");
            }
            catch(Exception e)
            {
                //log
                return new List<Door>();
            }
        }

        public async Task<bool> DoOpenAsync(int id)
        {
            try
            {
                var msg = new DoorActionMessage() { Id = id, DoorAction = DoorAction.Open };
                return await PostAsync<bool>("http://localhost:55664/api/DoorManagement/PerformDoorAction", msg);
            }
            catch (Exception e)
            {
                //log
                return false;
            }
        }
        public async Task<bool> DoCloseAsync(int id)
        {
            try
            {
                var msg = new DoorActionMessage() { Id = id, DoorAction = DoorAction.Close };
                return await PostAsync<bool>("http://localhost:55664/api/DoorManagement/PerformDoorAction", msg);
            }
            catch (Exception e)
            {
                //log
                return false;
            }
        }

        public async Task<bool> DoLockAsync(int id)
        {
            try
            {
                var msg = new DoorActionMessage() { Id = id, DoorAction = DoorAction.Lock};
                return await PostAsync<bool>("http://localhost:55664/api/DoorManagement/PerformDoorAction", msg);
            }
            catch (Exception e)
            {
                //log
                return false;
            }
        }
        public async Task<bool> DoUnLockAsync(int id)
        {
            try
            {
                var msg = new DoorActionMessage() { Id = id, DoorAction = DoorAction.UnLock };
                return await PostAsync<bool>("http://localhost:55664/api/DoorManagement/PerformDoorAction", msg);
            }
            catch (Exception e)
            {
                //log
                return false;
            }
        }

        private async Task<TResult> GetAsync<TResult>(string uri)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(uri);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.Timeout = TimeSpan.FromSeconds(10);

            var task = httpClient.GetAsync(httpClient.BaseAddress);
            await task;
            var result = task.Result;

            var jsonResult = result.Content.ReadAsStringAsync().Result;
            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TResult>(jsonResult);
            }
            else
            {
                throw new HttpRequestException($"{result.StatusCode}:{jsonResult}");
            }
        }

        private async Task<TResult> PostAsync<TResult>(string uri, object postContent)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(uri);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.Timeout = TimeSpan.FromSeconds(10);

            var stringContent = JsonConvert.SerializeObject(postContent);
            HttpContent httpContent = new StringContent(stringContent, Encoding.UTF8, "application/json");

            var task = httpClient.PostAsync(httpClient.BaseAddress, httpContent);
            await task;
            var result = task.Result;

            var jsonResult = result.Content.ReadAsStringAsync().Result;
            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TResult>(jsonResult);
            }
            else
            {
                throw new HttpRequestException($"{result.StatusCode}:{jsonResult}");
            }
        }

    }
}
