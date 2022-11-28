using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Admin_Client.Model.DB
{

    /*Placeholders for the Endpoints
     * https://localhost:7002/TblGroups
     * https://localhost:7002/TblGroups/{ID}
     * https://localhost:7002/TblGroupToMoneys
     * https://localhost:7002/TblLogins
     * https://localhost:7002/TblLogins/{ID}
     * https://localhost:7002/TblReceipts
     * https://localhost:7002/TblReceipts/{ID}
     * https://localhost:7002/TblTrips
     * https://localhost:7002/TblTrips/{ID}
     * https://localhost:7002/TblTripToUserExpenses
     * https://localhost:7002/TblTripToUserExpenses/{ID}
     * https://localhost:7002/TblUserExpenses
     * https://localhost:7002/TblUserExpenses/{ID}
     * https://localhost:7002/TblUsers
     * https://localhost:7002/TblUsers/{ID}
     * https://localhost:7002/TblUserToGroups
     * https://localhost:7002/TblUserToGroups/{ID}
     * 
     */
    public class HttpClientServices : IHttpImplementation
    {
        //Having it static reduces waste sockets and makes it faster
        private static readonly HttpClient _httpClient = new HttpClient();
        
        //Translator from to/from Json -> other format
        private readonly JsonSerializerOptions _options;

        

        public HttpClientServices()
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _httpClient.BaseAddress = new Uri("https://localhost:7270/");
           
        }

        //Simple console version that would do the job, now transform it into something that works with the API
        //Need to grab all bodies from 
        /*
        var group = new TblGroup
             {
                FldGroupId = 6,
                FldGroupName = "jsonTest",
                FldTempBool = true
             };
                string fileName = "testingGroup.json";
                FileStream createStream = File.Create(fileName);
                await JsonSerializer.SerializeAsync(createStream, group);
                
                Console.WriteLine(File.ReadAllText(fileName));
                //output: {"FldGroupId":"6","FldGroupName":jsonTest,"FldTempBool":"true"}
         * 
         * 
         */

        //use [Route], from API controllers for the specific tables
        //Translator from http to json
        //result: [{"fldGroupId":1,"fldGroupName":"Lars Test","fldTempBool":true},{"fldGroupId":3,"fldGroupName":"LarsKan","fldTempBool":true},{"fldGroupId":4,"fldGroupName":"Klasse2","fldTempBool":true}]

        public async Task Execute()
        {
            await GetTblGroups();
            await GetUsers();
        }


        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblGroup>>> DoTblGroups()
        {
            return await _options.TblGroups.ToListAsync();
        }
        */

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task GetTblGroups()
        {
            var response = await _httpClient.GetAsync("TblGroups");
            response.EnsureSuccessStatusCode();

            string fileName = "testingGroup.json";
            FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, response);

            var content = await response.Content.ReadAsStringAsync();
           
            var groups = JsonSerializer.Deserialize<List<TblGroup>>(content, _options);
            Console.WriteLine(groups);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task GetUsers()
        {
            var response = await _httpClient.GetAsync("tblUser");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<TblUser>>(content, _options);
        }

        /// <summary>
        /// CancelPendingRequests
        /// </summary>
        public void PostWithCancelSyntax()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("","parameter")
                    });
                    var response = client.PostAsync("/values", content).Result;
                    MessageBox.Show(response.ToString());
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Error");
                    }
                }
                catch(Exception Error)
                {
                    MessageBox.Show(Error.Message);
                }
                finally
                {
                    client.CancelPendingRequests();
                }
            }
        }

        /// <summary>
        /// Delete Async
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            HttpResponseMessage result;
            using (var client = new HttpClient())
            {
                try
                {
                    result = await client.DeleteAsync(endpoint);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        /// <summary>
        /// Get Async
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        async static Task<string> GetRequest(string url)
        {
            string con = "";
            using (HttpClient client = new HttpClient())
            {
                using(HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        con = await content.ReadAsStringAsync();
                        MessageBox.Show(con);
                    }
                }
            }
            return con;
        }

        /// <summary>
        /// Get Byte Array Async
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<string> DownloadBase64(string url)
        {
            var bytes = await new HttpClient().GetByteArrayAsync(url);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Get Stream Async
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<Stream> LoadServerResourceAsync(string url)
        {
            using (var _httpclient = new HttpClient())
            {
                var stream = await _httpclient.GetStreamAsync(url).ConfigureAwait(false);
                return stream;
            }
        }

       

        /// <summary>
        /// Post Async, used to submit an entity to a specified resource
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        async static Task<string> PostRequest(string url)
        {
            string response = "";
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7270/");
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("","paramter")
                });
                var result = await client.PostAsync("/api/values", content);
                response = await result.Content.ReadAsStringAsync();
            }
            return response;

        }

  

        /// <summary>
        /// Put Async
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        async static Task<string> PutRequest(string url)
        {
            string con = "";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7270/");
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("","parameter")
                });

                var result = await client.PutAsync("/api/values", content);
                con = await result.Content.ReadAsStringAsync();
            }
            return con;

        }

        /// <summary>
        /// Send Async
        /// </summary>
        /// <param name="i"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        static async Task<string> SendURI(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using( var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = u,
                    Content = c
                };

                HttpResponseMessage result = await client.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    response = result.StatusCode.ToString();
                }
            }
            return response;
        }

        


    }
}
