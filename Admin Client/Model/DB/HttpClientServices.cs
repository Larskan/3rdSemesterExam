using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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
using System.Net.Http.Headers;



namespace Admin_Client.Model.DB
{

    /*Placeholders for the Endpoints
     * Todo: Edit/Put/ReplaceRequest - Work in progress
     * https://localhost:7002/TblGroups
     * https://localhost:7002/TblLogins
     * https://localhost:7002/TblReceipts
     * https://localhost:7002/TblTrips
     * https://localhost:7002/TblUserExpenses
     * https://localhost:7002/TblUsers
     */
    public class HttpClientServices
    {
        //Having it static reduces waste sockets and makes it faster
        private static readonly HttpClient _httpClient = new HttpClient();
        

        public HttpClientServices()
        {
           _httpClient.BaseAddress = new Uri("https://localhost:7270/");
            #region From attempt at making more compact code, work in progress
            //var serviceCollection = new ServiceCollection();
            //ConfigureServices(serviceCollection);
            //var services = serviceCollection.BuildServiceProvider();
            //var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();

            //grabs the service matching group, using its base address
            //var httpClientGroups = httpClientFactory.CreateClient("group");
            //var responseMessage = await httpClientGroups.GetAsync("");
            //TestingCompact().AsyncState.ToString();
            #endregion
        }

        #region Attempt at making more clean and compact code, work in progress
        public async static Task TestingCompact()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var services = serviceCollection.BuildServiceProvider();
            var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();

            //grabs the service matching group, using its base address
            var httpClientGroups = httpClientFactory.CreateClient("group");
            var responseMessage = await httpClientGroups.GetAsync("");
            responseMessage.EnsureSuccessStatusCode();

            var httpClientUsers = httpClientFactory.CreateClient("user");
            var responseMessage2 = await httpClientUsers.GetAsync("");
            responseMessage2.EnsureSuccessStatusCode();

            Console.WriteLine("END");
        }

        //Work in progress to create a more compact code
        private static void ConfigureServices(ServiceCollection services)
        {
            //Names Client
            //Whenever I make HttpClient request through "name", it will use the correct table Base Address
            services.AddHttpClient("groups",options =>
            {
                options.BaseAddress = new Uri("https://localhost:7002/TblGroups");
                //Accept header for Json format
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddHttpClient("login", options =>
            {
                options.BaseAddress = new Uri("https://localhost:7002/TblLogins");
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddHttpClient("receipt", options =>
            {
                options.BaseAddress = new Uri("https://localhost:7002/TblReceipts");
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddHttpClient("trip", options =>
            {
                options.BaseAddress = new Uri("https://localhost:7002/TblTrips");
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddHttpClient("expense", options =>
            {
                options.BaseAddress = new Uri("https://localhost:7002/TblUserExpenses");
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddHttpClient("user", options =>
            {
                options.BaseAddress = new Uri("https://localhost:7002/TblUsers");
                options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

        }
        #endregion

        #region Get specific from table
        /// <summary>
        /// Gets the Json Data, turns it into Http data for an ID(Used in Get Specific from Table Region)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private async static Task<string> GetHttpResponse(string url, int id)
        {
            string res = url + "/" + id;
            Debug.WriteLine("Combination: " + res);
            _httpClient.BaseAddress = new Uri(res);
            var response = await _httpClient.GetAsync(res);
            Debug.WriteLine("Response2: " + response);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Debug.WriteLine("Final2: " + content);
            return content;
        }

        [HttpGet("{id}")]
        public Task GetSpecificLogin(int id)
        {
            _ = GetHttpResponse("https://localhost:7002/TblLogins", id);
            return Task.CompletedTask;
        }
        [HttpGet("{id}")]
        public Task GetSpecificUser(int id)
        {
            _ = GetHttpResponse("https://localhost:7002/TblUsers", id);
            return Task.CompletedTask;
        }
        [HttpGet("{id}")]
        public Task GetSpecificTrip(int id)
        {
            _ = GetHttpResponse("https://localhost:7002/TblTrips", id);
            return Task.CompletedTask;
        }
        [HttpGet("{id}")]
        public Task GetSpecificReceipt(int id)
        {
            _ = GetHttpResponse("https://localhost:7002/TblReceipts", id);
            return Task.CompletedTask;
        }
        [HttpGet("{id}")]
        public Task GetSpecificExpense(int id)
        {
            _ = GetHttpResponse("https://localhost:7002/TblUserExpenses", id);
            return Task.CompletedTask;
        }
        [HttpGet("{id}")]
        public Task GetSpecificGroup(int id) //WORKS
        {
            _ = GetHttpResponse("https://localhost:7002/TblGroups", id);
            return Task.CompletedTask;
        }
        #endregion

        #region Add to Table

        [HttpPost]
        public Task AddGroup(string name, bool boll)
        {
            _ = PostHttpNewGroup("https://localhost:7002/TblGroups", name, boll);
            return Task.CompletedTask;
        }
        [HttpPost]
        public Task AddUser(string mail, string firstName, string lastName, int phone, bool admin)
        {
            _ = PostHttpNewUser("https://localhost:7002/TblUsers",mail, firstName, lastName, phone, admin);
            return Task.CompletedTask;
        }
        [HttpPost]
        public Task AddLogin(int userID, string pass)
        {
            _ = PostHttpNewLogin("https://localhost:7002/TblLogins", userID, pass);
            return Task.CompletedTask;
        }
        [HttpPost]
        public Task AddReceipt(int userID, int tripID, double value, double paid)
        {
            _ = PostHttpNewReceipt("https://localhost:7002/TblReceipts", userID, tripID, value, paid);
            return Task.CompletedTask;
        }
        [HttpPost]
        public Task AddTrip(double sum)
        {
            _ = PostHttpNewTrip("https://localhost:7002/TblTrips",sum);
            return Task.CompletedTask;
        }
        [HttpPost]
        public Task AddUserExpense(int userID, double expense, string note, DateTime date)
        {
            _ = PostHttpNewUserExpense("https://localhost:7002/TblUserExpenses",userID, expense, note, date);
            return Task.CompletedTask;
        }
        #endregion

        #region Get All from a Table

        /// <summary>
        /// Gets the Json Data, turns it into Http data for entire table
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
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
        public Task GetAllTblGroups()  //WORKS
        {
            //Grabs the response and turnd it to http
            _ = GetAllHttpResponse("https://localhost:7002/TblGroups");
            Debug.WriteLine("CHECK4: " + GetAllHttpResponse("https://localhost:7002/TblGroups"));
            return Task.CompletedTask;
        }
        #endregion

        #region Delete data from Table

        [HttpDelete]
        private static HttpResponseMessage ClientDeleteRequest(string RequestURI, int ID)
        {
            string res = RequestURI + "/" + ID;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = client.DeleteAsync(res).Result;
            return response;
        }

        public Task DeleteGroup(int id)
        {
            ClientDeleteRequest("https://localhost:7002/TblGroups", id);
            return Task.CompletedTask;
        }
        public Task DeleteLogin(int id)
        {
            ClientDeleteRequest("https://localhost:7002/TblLogins", id);
            return Task.CompletedTask;
        }
        public Task DeleteReceipt(int id)
        {
            ClientDeleteRequest("https://localhost:7002/TblReceipts", id);
            return Task.CompletedTask;
        }
        public Task DeleteTrip(int id)
        {
            ClientDeleteRequest("https://localhost:7002/TblTrips", id);
            return Task.CompletedTask;
        }
        public Task DeleteUserExpenses(int id)
        {
            ClientDeleteRequest("https://localhost:7002/TblUserExpenses", id);
            return Task.CompletedTask;
        }
        public Task DeleteUser(int id)
        {
            ClientDeleteRequest("https://localhost:7002/TblUsers", id);
            return Task.CompletedTask;
        }
        #endregion

        #region Post new entry to a Table
        [HttpPost]
        /// <summary>
        /// Posts the data as a Json for Group
        /// </summary>
        /// <param name="url">Api/TblGroup</param>
        /// <param name="id">fldGroupID</param>
        /// <param name="name">fldGroupName</param>
        /// <param name="groupBool">fldGroupBoolean</param>
        /// <returns></returns>
        public async static Task<string> PostHttpNewGroup(string url, string name, bool groupBool)
        {
            var endpoint = _httpClient.BaseAddress = new Uri(url);

            //No ID due to identity having auto increment
            var newPost = new TblGroup()
            {
                FldGroupName = name,
                FldGroupBoolean = groupBool

            };

            //Convert the new posting to Json
            var newPostJson = JsonConvert.SerializeObject(newPost);
            Debug.WriteLine("Target" + newPostJson);
            //StringContent: Formatted text appropriate for the http server/client communication.
            var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
            Debug.WriteLine("Payload" + payload);
            var result = await _httpClient.PostAsync(endpoint, payload);
            var final = await result.Content.ReadAsStringAsync();

            Debug.WriteLine("Final: " + final);
            return final;
            
        }

        [HttpPost]
        /// <summary>
        /// Posts the data as a Json for User
        /// </summary>
        /// <param name="url">Api/TblUser</param>
        /// <param name="mail">fldEmail</param>
        /// <param name="firstName">FldFirstName</param>
        /// <param name="lastName">FldLastName</param>
        /// <param name="phone">FldPhonenumber</param>
        /// <param name="admin">FldIsAdmin</param>
        /// <returns></returns>
        public async static Task<string> PostHttpNewUser(string url, string mail, string firstName, string lastName, int phone, bool admin)
        {
            var endpoint = _httpClient.BaseAddress = new Uri(url);
            var newPost = new TblUser()
            {
                FldEmail = mail,
                FldFirstName = firstName,
                FldLastName = lastName,
                FldPhonenumber = phone,
                FldIsAdmin = admin
            };
            //Convert the new posting to Json
            var newPostJson = JsonConvert.SerializeObject(newPost);
            //StringContent: Formatted text appropriate for the http server/client communication.
            var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(endpoint, payload);
            var final = await result.Content.ReadAsStringAsync();
            return final;
        }

        [HttpPost]
        /// <summary>
        /// Posts the data as a Json for Login
        /// </summary>
        /// <param name="url">Api/TblUser</param>
        /// <param name="userID">FldUserId</param>
        /// <param name="pass">FldPassword</param>
        /// <returns></returns>
        public async static Task<string> PostHttpNewLogin(string url, int userID, string pass)
        {
            var endpoint = _httpClient.BaseAddress = new Uri(url);
            var newPost = new TblLogin()
            {
                FldUserId = userID,
                FldPassword = pass
            };
            //Convert the new posting to Json
            var newPostJson = JsonConvert.SerializeObject(newPost);
            //StringContent: Formatted text appropriate for the http server/client communication.
            var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(endpoint, payload);
            var final = await result.Content.ReadAsStringAsync();
            return final;
        }

        [HttpPost]
        /// <summary>
        /// Posts the data as a Json for Receipt
        /// </summary>
        /// <param name="url">Api/TblReceipt</param>
        /// <param name="userID">FldUserId</param>
        /// <param name="tripID">FldTripId</param>
        /// <param name="value">FldProjectedValue</param>
        /// <param name="paid">FldAmountPaid</param>
        /// <returns></returns>
        public async static Task<string> PostHttpNewReceipt(string url, int userID, int tripID, double value, double paid)
        {
            var endpoint = _httpClient.BaseAddress = new Uri(url);
            var newPost = new TblReceipt()
            {
                FldUserId = userID,
                FldTripId = tripID,
                FldProjectedValue = value,
                FldAmountPaid = paid
            };
            //Convert the new posting to Json
            var newPostJson = JsonConvert.SerializeObject(newPost);
            //StringContent: Formatted text appropriate for the http server/client communication.
            var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(endpoint, payload);
            var final = await result.Content.ReadAsStringAsync();
            return final;

        }

        [HttpPost]
        /// <summary>
        /// Posts the data as a Json for Trip
        /// </summary>
        /// <param name="url">Api/TblTrip</param>
        /// <param name="sum">FldSum</param>
        /// <returns></returns>
        public async static Task<string> PostHttpNewTrip(string url, double sum)
        {
            var endpoint = _httpClient.BaseAddress = new Uri(url);
            var newPost = new TblTrip()
            {
                FldSum = sum
            };
            //Convert the new posting to Json
            var newPostJson = JsonConvert.SerializeObject(newPost);
            //StringContent: Formatted text appropriate for the http server/client communication.
            var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(endpoint, payload);
            var final = await result.Content.ReadAsStringAsync();
            return final;
        }

        [HttpPost]
        /// <summary>
        /// Posts the data as a Json for User Expense
        /// </summary>
        /// <param name="url">Api/TblUserExpenses</param>
        /// <param name="userID">FldUserId</param>
        /// <param name="expense">FldExpense</param>
        /// <param name="note">FldNote</param>
        /// <param name="date">FldDate</param>
        /// <returns></returns>
        public async static Task<string> PostHttpNewUserExpense(string url, int userID, double expense, string note, DateTime date)
        {
            var endpoint = _httpClient.BaseAddress = new Uri(url);
            var newPost = new TblUserExpense()
            {
                FldUserId = userID,
                FldExpense = expense,
                FldNote = note,
                FldDate = date
            };
            //Convert the new posting to Json
            var newPostJson = JsonConvert.SerializeObject(newPost);
            //StringContent: Formatted text appropriate for the http server/client communication.
            var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(endpoint, payload);
            var final = await result.Content.ReadAsStringAsync();
            return final;
        }

        #endregion

        #region Put/Replace data with new data - Not Done
        public async static Task<string> ReplaceContentHttp(string url)
        {
            //var endpoint = _httpClient.BaseAddress = new Uri(url);
            string container = "";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("","parameter")
                });
                var result = await client.PutAsync("/values", content);
                container = await result.Content.ReadAsStringAsync();
            }
            return container;

        }
        #endregion

        /// <summary>
        /// Put Async - Incomplete code
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

    }
}
