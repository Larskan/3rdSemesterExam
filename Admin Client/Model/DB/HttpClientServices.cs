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
using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using System.Threading;
using System.Security.Policy;

namespace Admin_Client.Model.DB
{

    /*Placeholders for the Endpoints
     * Todo: Edit/Put/ReplaceRequest - Work in progress
     * Todo: If I grab a groupID I need to get foreign keys too(Users connected to a group)
     * Todo: Dictionaries
     * Todo: Make User and Group communicate through tblUserToGroup 
     * (User and Group was many to many, but by adding TblUserToGroup we get 2 seperate 1 to many)(coupling)
     * Use LINQ for this
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
        public async Task TestingCompact()
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
        private async Task<string> GetHttpResponse(string url, int id)
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
            //Needs to get entire group but also each user connected to the group
            _ = GetHttpResponse("https://localhost:7002/TblGroups", id);
            return Task.CompletedTask;
        }
        [HttpGet("{id}")]
        public Task GetGroupNameAndUsersWithGroupID(int id)
        {
            TblUserToGroup tbl = new TblUserToGroup
            {
                FldGroupId = id
            };

            UpdateGroupID(tbl);
            UpdateUserID(tbl);
            GetAllTblUserToGroup(id);
            return Task.CompletedTask;
        }
        [HttpGet("{id}")]
        public Task GetUserNameAndGroupsWithUserID(int id)
        {
            TblUserToGroup tbl = new TblUserToGroup();
            UpdateGroupID(tbl);
            UpdateUserID(tbl);
            GetAllTblUserToGroup(id);
            return Task.CompletedTask;
        }

        #endregion

        #region testing
        //TESTING METHOD FOR GETTING USERS AND GROUPS TO DISPLAY TOGETHER, depending on which ID is used
        [HttpGet("{id}")]
        public Task GetGroupAndUsers(int id)
        {
            TblGroup dot = new TblGroup();
            TblUser dut = new TblUser();

            //Can change to switch once it works

            if(id == dot.FldGroupId)
            {
                //Display group + all users part of that group (1 group has many users)
                _ = GetHttpResponse("https://localhost:7002/TblUserToGroup", dot.FldGroupId);
                return Task.CompletedTask;
            }
            else if(id == dut.FldUserId)
            {
                //Display user + all groups that user is part of (1 user has many groups)
                _ = GetHttpResponse("https://localhost:7002/TblUserToGroup", dut.FldUserId);
            }
            return Task.CompletedTask;
        }


        [HttpGet("{id}")]
        public Task InnerJoin(int id)
        {
            List<TblGroup> groups = new List<TblGroup>();
            List<TblUser> users = new List<TblUser>();
            List<TblUserToGroup> combined = new List<TblUserToGroup>();
            Debug.WriteLine("List groups: " + groups.Count);
            Debug.WriteLine("List users: " + users.Count); 

            TblGroup dot = new TblGroup();
            TblUser dut = new TblUser();
            TblUserToGroup dit = new TblUserToGroup();
            

            if (id == dot.FldGroupId)
            {
                //Display group + all users part of that group (1 group has many users)
                _ = GetHttpResponse("https://localhost:7002/TblUserToGroup", dot.FldGroupId);

                IEnumerable<TblGroup> matchID = (from TblGroup groupItemGroup in groups
                                                 join TblUser groupItemUser in users
                                                 on groupItemGroup.FldGroupId
                                                 equals groupItemUser.FldUserId
                                                 select groupItemGroup);

                
                /*
                var q = (from tg in TblGroup
                         join tutg in TblUserToGroup on tg.FldGroupId equals tutg.FldGroupId
                         join tu in TblUser on tutg.FldUserId equals tu.FldUserId
                         orderby tutg.FldUserToGroupId
                         select new
                         {
                             tutg.FldUserToGroupID,
                             tg.FldGroupId,
                             tg.FldGroupName,
                             tu.FldUserId,
                         });
                */


                //OutputCollectionToConsole(matchID);
                //Console.ReadKey();

                return (Task)matchID;
            }
            else if (id == dut.FldUserId)
            {
                //Display user + all groups that user is part of (1 user has many groups)
                _ = GetHttpResponse("https://localhost:7002/TblUserToGroup", dut.FldUserId);

                IEnumerable<TblUser> matchID = (from TblUser groupItemUser in users
                                                join TblGroup groupItemGroup in groups
                                                on groupItemUser.FldUserId
                                                equals groupItemGroup.FldGroupId
                                                select groupItemUser);
                OutputCollectionToConsole(matchID);
                Console.ReadKey();
                return (Task)matchID;
            }

           
            //else if(id == dut.FldUserId && dot.FldGroupId){  }
            //return (Task)matchID;
            return Task.CompletedTask;
        }

        private static void OutputCollectionToConsole(IEnumerable<object> collectionToOutput)
        {
            foreach(object collectionItem in collectionToOutput)
                Console.WriteLine(collectionItem.ToString());
        }
        #endregion

        #region Updating Tables
        private int UpdateUserID(TblUserToGroup utg)
        {
            TblUser user = new TblUser();
            //GetAllTblUsers();
            return utg.FldUserId = user.FldUserId;
            //return Task.CompletedTask;

        }
        private int UpdateGroupID(TblUserToGroup utg)
        {
            TblGroup group = new TblGroup();
            return utg.FldGroupId= group.FldGroupId;
            //GetAllTblGroups();
            //utg.FldGroupId = GetAllTblGroups().Id;
            //return Task.CompletedTask;
        }
        #endregion


        #region Add to Table

        [HttpPost]
        public Task AddGroup(TblGroup group)
        {
            string name = group.FldGroupName = "";
            bool boll = (bool)(group.FldGroupBoolean = true);
            //string name, bool boll
            _ = PostHttpNewGroup("https://localhost:7002/TblGroups", name, boll);
            return Task.CompletedTask;
        }
        [HttpPost]
        public Task AddUser(TblUser user)
        {
            string mail = user.FldEmail;
            string fName = user.FldFirstName;
            string lName = user.FldLastName;
            int phone = user.FldPhonenumber;
            bool admin = user.FldIsAdmin;

            //string mail, string firstName, string lastName, int phone, bool admin
            _ = PostHttpNewUser("https://localhost:7002/TblUsers",mail, fName, lName, phone, admin);
            return Task.CompletedTask;
        }
        [HttpPost]
        public Task AddLogin(TblLogin login)
        {
            int id = login.FldUserId;
            string pass = login.FldPassword;
            //int userID, string pass
            _ = PostHttpNewLogin("https://localhost:7002/TblLogins", id, pass);
            return Task.CompletedTask;
        }
        [HttpPost]
        public Task AddReceipt(TblReceipt receipt)
        {
            int userID = receipt.FldUserId;
            int tripID = receipt.FldTripId;
            double value = receipt.FldProjectedValue;
            double paid = receipt.FldAmountPaid;
            //int userID, int tripID, double value, double paid
            _ = PostHttpNewReceipt("https://localhost:7002/TblReceipts", userID, tripID, value, paid);
            return Task.CompletedTask;
        }
        [HttpPost]
        public Task AddTrip(TblTrip trip)
        {
            double sum = trip.FldSum;
            _ = PostHttpNewTrip("https://localhost:7002/TblTrips",sum);
            return Task.CompletedTask;
        }
        [HttpPost]
        public Task AddUserExpense(TblUserExpense userExpense)
        {
            int userID = userExpense.FldUserId;
            double expense = userExpense.FldExpense;
            string note = userExpense.FldNote;
            DateTime date = userExpense.FldDate;
            //int userID, double expense, string note, DateTime date
            _ = PostHttpNewUserExpense("https://localhost:7002/TblUserExpenses",userID, expense, note, date);
            return Task.CompletedTask;
        }
        #endregion

        #region Get All from a Table

        [HttpGet]
        private async Task<string> GetAllHttpResponse(string url)
        {
            //Create a base Get Response
            _httpClient.BaseAddress = new Uri(url);
            //Get async request to chosen url and save it as response
            var response = await _httpClient.GetAsync(url);
            Debug.WriteLine("Target: " + url);

            //Makes sure that it gets a 200 code OK
            response.EnsureSuccessStatusCode();
            Debug.WriteLine("Response: " + response);

            //Testing
            //var responseBody = response.Content.ReadAsStringAsync().Result;
            //var myList = JsonConvert.DeserializeObject<List<string>>(responseBody);

            //Makes the response into HTTP through serialization
            var content = await response.Content.ReadAsStringAsync();
            //List<string> pot = new List<string> { content };
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
            return Task.CompletedTask;
        }

        [HttpGet]
        public Task GetAllTblUserToGroup(int id)
        {
            _ = GetAllHttpResponse("https://localhost:7002/TblUserToGroups"+"/"+id);
            return Task.CompletedTask;
        }
        #endregion

        #region Delete data from Table

        [HttpDelete]
        public HttpResponseMessage ClientDeleteRequest(string group, int ID)
        {
            var address = _httpClient.BaseAddress = new Uri("https://localhost:7002/");
            string res = address + group + "/" + ID;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = client.DeleteAsync(res).Result;
            return response;
        }
        #endregion

        #region Post new entry to a Table

        [HttpPost]
        public async Task<string> PostHttpNewGroup(string url, string name, bool groupBool)
        {
            var endpoint = _httpClient.BaseAddress = new Uri(url);
            var newPost = new TblGroup()
            {
                FldGroupName = name,
                FldGroupBoolean = groupBool
            };

            //Convert the new posting to Json
            var newPostJson = JsonConvert.SerializeObject(newPost);
            //StringContent: Formatted text appropriate for the http server/client communication.
            var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");
            //Debug.WriteLine("Payload" + payload);
            var result = await _httpClient.PostAsync(endpoint, payload);
            var final = await result.Content.ReadAsStringAsync();

            return final;  
        }

        [HttpPost]
        public async Task<string> PostHttpNewUser(string url, string mail, string firstName, string lastName, int phone, bool admin)
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
        public async Task<string> PostHttpNewLogin(string url, int userID, string pass)
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
        public async Task<string> PostHttpNewReceipt(string url, int userID, int tripID, double value, double paid)
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
        public async Task<string> PostHttpNewTrip(string url, double sum)
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
        public async Task<string> PostHttpNewUserExpense(string url, int userID, double expense, string note, DateTime date)
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
        public async Task<string> ReplaceContentHttp(string url)
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
        async  Task<string> PutRequest(string url)
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
