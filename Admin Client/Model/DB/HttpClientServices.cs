using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Admin_Client.Model.DB
{

    /*Placeholders for the Endpoints
     * TODO: Get One, Get all, Put, Post, Delete, Edit
     * TODO: GetRequest
     * TODO: GetAllRequest - X
     * Todo: PostRequest
     * Todo: DeleteRequest
     * Todo: EditRequest
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
    public class HttpClientServices
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

        /*
        public async Task Execute()
        {
            await GetAllTblGroups();
            await GetUsers();
        }
        (For testing)
        HttpClientServices s = new HttpClientServices();
            s.GetAllTblGroups();
            Debug.WriteLine("RESULT1: " + s);
            HttpClientServices s2 = new HttpClientServices();
            s2.GetSpecificGroup(1);
            Debug.WriteLine("RESULT2: " + s2);
        */

        #region Get specific from table
        [HttpGet("{id}")]
        public Task GetSpecificLogin()
        {
            return Task.CompletedTask;
        }
        [HttpGet("{id}")]
        public Task GetSpecificUser()
        {
            return Task.CompletedTask;
        }
        [HttpGet("{id}")]
        public Task GetSpecificTrip()
        {
            return Task.CompletedTask;
        }
        [HttpGet("{id}")]
        public Task GetSpecificReceipt()
        {
            return Task.CompletedTask;
        }
        [HttpGet("{id}")]
        public Task GetSpecificExpense()
        {
            return Task.CompletedTask;
        }
        [HttpGet("{id}")]
        public Task GetSpecificGroup(int id)
        {
            //var tblGroup = await _context.TblGroups.FindAsync(id);
            
            _ = GetHttpResponse("https://localhost:7002/TblGroups", id);
            return Task.CompletedTask;
        }
        #endregion

        #region Get All from a Table
        [HttpGet]
        public Task GetAllTblLogin()
        {
            _ = GetAllHttpResponse("https://localhost:7002/TblLogins");
            return Task.CompletedTask;
        }
        [HttpGet]
        public Task GetAllTblReceipts()
        {
            _ = GetAllHttpResponse("https://localhost:7002/TblReceipts");
            return Task.CompletedTask;
        }
        [HttpGet]
        public Task GetAllTblTrips()
        {
            _ = GetAllHttpResponse("https://localhost:7002/TblTrips");
            return Task.CompletedTask;
        }
        [HttpGet]
        public Task GetAllTblUserExpenses()
        {
            _ = GetAllHttpResponse("https://localhost:7002/TblTripToUserExpenses");
            return Task.CompletedTask;
        }
        [HttpGet]
        public Task GetAllTblUsers()
        {
            _ = GetAllHttpResponse("https://localhost:7002/TblUsers");
            return Task.CompletedTask;
        }

        [HttpGet]
        public Task GetAllTblGroups()
        {
            //Grabs the response and turnd it to http
            _ = GetAllHttpResponse("https://localhost:7002/TblGroups");
            Debug.WriteLine("CHECK4: " + GetAllHttpResponse("https://localhost:7002/TblGroups"));
            return Task.CompletedTask;
            //Grabs the response and turnd it to http, same as above, different version
            _ = GetRequest("https://localhost:7002/TblGroups");
            Debug.WriteLine(GetRequest("CHECK5: " + "https://localhost:7002/TblGroups"));
            return Task.CompletedTask;
        }
        #endregion

        private async static Task<string> GetAllHttpResponse(string url)
        {
            //Create a base Get Response
            _httpClient.BaseAddress = new Uri(url);
            //Get async request to chosen url and save it as response
            var response = await _httpClient.GetAsync(url);
            Debug.WriteLine("Target: " + url);
          
            //Makes sure that it gets a 200 code OK
            response.EnsureSuccessStatusCode();
            Debug.WriteLine("Response: " + response);

            //Makes the response into HTTP through serialization
            var content = await response.Content.ReadAsStringAsync();
            Debug.WriteLine("Final: " + content);
            return content;

        }
        private async static Task<string> GetHttpResponse(string url, int id)
        {
            string res = url + "/" +  id;
            Debug.WriteLine("Combination: "+res);
            _httpClient.BaseAddress = new Uri(res);
            var response = await _httpClient.GetAsync(res);
            Debug.WriteLine("Response2: " + response);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Debug.WriteLine("Final2: " + content);
            return content;
        }

        private async static Task<string> PostHttpResponse(string url)
        {
            _httpClient.BaseAddress = new Uri(url);

            var response = await _httpClient.GetAsync(url);
            Debug.WriteLine("Target" + url);
            response.EnsureSuccessStatusCode();
            Debug.WriteLine("Response" + response);

            var content = await response.Content.ReadAsStringAsync();
            Debug.WriteLine("Content" + content);
            //var post = JsonSerializer.Deserialize < List < (url) >> (content, _options);


            Debug.WriteLine("Final: " + response);
            return content;
            
        }

   

        /// <summary>
        /// Get Async
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        async static Task<string> GetRequest(string url)
        {
            string con = "";
            using (_httpClient)
            {
                using(HttpResponseMessage response = await _httpClient.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        con = await content.ReadAsStringAsync();

                    }
                }
            }
            /*
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
            */
            return con;
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
