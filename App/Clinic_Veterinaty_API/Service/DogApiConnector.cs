using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Clinic_Veterinaty_API.DTO;
using Newtonsoft.Json;


namespace Clinic_Veterinaty_API.Service
{
    public class DogApiConnector
    {
        public List<dynamic> ConnectToAPI(string typeService, string page)
        {
                  
            using (var client = new HttpClient())
            {
                string url = "https://api.thedogapi.com/v1/" + typeService  + page;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("x-api-key", "42c337bb-d213-4a59-aafa-97d9c856d8f9");
                var response = client.GetStringAsync(url);
                response.Wait();
                var responseList = JsonConvert.DeserializeObject<List<dynamic>>(response.Result);
                return responseList;
            }
        }
        // public List<dynamic> ConnectToAPI(string typeService,  string nameBreed)
        // {                      
        //     using (var client = new HttpClient())
        //     {
        //         string url = "https://api.thedogapi.com/v1/" + typeService +"?q=" + nameBreed;
        //         client.DefaultRequestHeaders.Clear();
        //         client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("x-api-key", "42c337bb-d213-4a59-aafa-97d9c856d8f9");
        //         var response = client.GetStringAsync(url);
        //         response.Wait();
        //         var responseList = JsonConvert.DeserializeObject<List<dynamic>>(response.Result);
        //         return responseList;
        //     }
        // }
    }
}