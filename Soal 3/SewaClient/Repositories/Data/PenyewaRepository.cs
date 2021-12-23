using Newtonsoft.Json;
using SewaAPI.Models;
using SewaAPI.ViewModel;
using SewaClient.Base.Urls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SewaClient.Repositories.Data
{
    public class PenyewaRepository : GeneralRepository<Penyewa, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        public PenyewaRepository(Address address, string request = "Penyewas/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };

        }

        public async Task<List<LogHistoryVM>> LogHistory()
        {
            List<LogHistoryVM> entities = new List<LogHistoryVM>();

            using (var response = await httpClient.GetAsync(request+ "LogHistory"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<LogHistoryVM>>(apiResponse);
            }
            return entities;
        }
        public async Task<LogHistoryVM> LogHistoryId(int id)
        {
            LogHistoryVM entity = null;

            using (var response = await httpClient.GetAsync(request + "LogHistory/" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<LogHistoryVM>(apiResponse);
            }
            return entity;
        }

        public HttpStatusCode Register(RegisterVM registerVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request+ "Register", content).Result;
            return result.StatusCode;
        }

        public async Task<PenyewaVM> Penyewa(int PenyewaId)
        {
            PenyewaVM entity = null;

            using (var response = await httpClient.GetAsync(request +"Penyewa/"+ PenyewaId))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<PenyewaVM>(apiResponse);
            }
            return entity;
        }



    }
}
