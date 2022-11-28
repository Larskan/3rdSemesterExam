using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Admin_Client.Model.DB
{
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

        //use [Route], from API controllers for the specific tables
        //Translator from http to json
        //result: [{"fldGroupId":1,"fldGroupName":"Lars Test","fldTempBool":true},{"fldGroupId":3,"fldGroupName":"LarsKan","fldTempBool":true},{"fldGroupId":4,"fldGroupName":"Klasse2","fldTempBool":true}]

        public async Task Execute()
        {
            await GetGroups();
            await GetUsers();
        }
        /// <summary>
        /// Supposed to create a two-way link between API and DB, at the moment it is one-way
        /// </summary>
        /// <returns></returns>
        [Route("TblGroup")]
        public async Task GetGroups()
        {
            var response = await _httpClient.GetAsync("tblGroup");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var groups = JsonSerializer.Deserialize<List<TblGroup>>(content, _options);
        }

        /// <summary>
        /// Supposed to create a two-way link between API and DB, at the moment it is one-way
        /// </summary>
        /// <returns></returns>
        [Route("TblUser")]
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
                    var response = client.PostAsync("/api/values", content).Result;
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
        async static Task<string> getRequest(string url)
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
                client.BaseAddress = new Uri("http://localhost:7270/swagger");
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
                client.BaseAddress = new Uri("http://localhost:7270/swagger");
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
