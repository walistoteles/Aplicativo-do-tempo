using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TimeApp.Services
{
    class ApiCaller
    {


        public static async Task<ApiResponse> Get(string url, string authID = null)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(authID))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", authID);
                }

                var request = await client.GetAsync(url);
                if (request.IsSuccessStatusCode)
                {
                    return new ApiResponse { Response = await request.Content.ReadAsStringAsync() };
                }
                else
                {
                    return new ApiResponse { ErrorMessage = request.ReasonPhrase };
                }
            }
        }

        public class ApiResponse
        {
            public bool Succesful => ErrorMessage == null;
            public string ErrorMessage { get; set; }
            public string Response { get; set; }

        }
    }

}
