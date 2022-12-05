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
using System.Windows.Input;
using Admin_Client.Model.DB.EF_Test;
using System.Data.Entity;
using JsonSerializer = System.Text.Json.JsonSerializer;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Web.Http.ModelBinding.Binders;

namespace Admin_Client.Model.DB
{
    /*
     * Todo: Edit/Put/ReplaceRequest - Needs testing for group, if works, add for rest
     * Todo: If I grab a groupID I need to get foreign keys too(Users connected to a group) - Should work, need test
     */
    public class HttpClientServices : HttpDbContext
    {
        //Having it static reduces waste sockets and makes it faster
        private static readonly HttpClient _httpClient = new HttpClient();

        List<object> Buffer = new List<object>();



        public HttpClientServices()
        {
            _httpClient.BaseAddress = new Uri("https://localhost:7270/");
        }


        #region Testing - Hardcoding attempt for group+its users
        //Attempt at Hardcoding
        [HttpGet("{id}")]
        public Task GetGroupAndUsers(int id)
        {
            tblGroup dot = new tblGroup();
            tblUser dut = new tblUser();

            //Can change to switch once it works

            if (id == dot.fldGroupID)
            {
                //Display group + all users part of that group (1 group has many users)
                _ = GetHttpResponse("TblUserToGroups", dot.fldGroupID);
                _ = GetHttpResponse("TblGroups", dot.fldGroupID);
                _ = GetHttpResponse("TblUsers", dut.fldUserID);
                while (true)
                {
                    _ = GetAllHttpResponse("TblUsers");
                }
                return Task.CompletedTask;
            }
            else if (id == dut.fldUserID)
            {
                //Display user + all users that user is part of (1 user has many users)
                _ = GetHttpResponse("TblUserToGroups", dut.fldUserID);
            }
            return Task.CompletedTask;
        }
        #endregion
        #region Testing - Collection Output attempt
        
        private static void OutputCollectionToConsole(IEnumerable<object> collectionToOutput)
        {
            foreach (object collectionItem in collectionToOutput)
                Console.WriteLine(collectionItem.ToString());
        }
        #endregion
        #region Testting - Grab group+its users and user+their groups with inner join
        public Task<string> TestingGrab(string group, int id)
        {
            tblUser bruger = new tblUser();
            tblGroup gruppe = new tblGroup();
            var handler = GetHttpResponse(group, id);
            var address = _httpClient.BaseAddress = new Uri("https://localhost:7002/");
            var res = address + group + "/" + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var innerJoinQuery =
                from g in TblGroups
                where g.fldGroupID.Equals(id)
                join gtu in TblUserToGroups on g.fldGroupID equals gtu.fldGroupID
                join gtut in TblUsers on gtu.fldUserID equals gtut.fldUserID
                select new
                {
                    gtu.fldGroupID,
                    gtut.fldUserID,
                    gtut.fldFirstName
                };

            var a = JsonConvert.DeserializeAnonymousType(res, innerJoinQuery);
            Debug.WriteLine("aaaaaa: " + a);
            //var b = JsonConvert.DeserializeObject(innerJoinQuery.ToString());
            //Debug.WriteLine("bbbbb: " + b);
            Debug.WriteLine("InnerJoinQuery: " + innerJoinQuery);
            var content = client.GetAsync(a.ToString()).Result;

            using (StreamReader sr = new StreamReader(content.Content.ReadAsStreamAsync().Result))
            {
                Debug.WriteLine("WAAAAHHHH: " + sr.ReadToEnd());
            }

            Debug.WriteLine("Content: " + content);
            Debug.WriteLine("ContentToString: " + content.ToString());
            return Task.FromResult(content.ToString());
        }

        //Select entire group + all user ID's part of that group - In theory it should work
        public async Task<List<object>> TestingGrabGroupAndUsers(int input)
        {
            //grab group and its users
            /*
             * SQL Version:
             * select *
             * from(tblGroup
             * inner join tblUserToGroup on tblGroup.fldGroupID = tblUserToGroup.fldGroupUD)
             * inner join tblUser on tblUserToGroup.fldUserID = tblUser.fldUserID
             * where tblUser.fldUserID = '1';
             */

            var result = (from g in TblGroups
                          where g.fldGroupID.Equals(input)
                          join gtu in TblUserToGroups on g.fldGroupID
                          equals gtu.fldGroupID
                          join gtut in TblUsers on gtu.fldUserID equals gtut.fldUserID
                          select new
                          {
                              gtu.fldGroupID,
                              gtu.fldUserID
                          });
            Debug.WriteLine("Result1: " + result.ToString());
            Debug.WriteLine("Result1: " + result);
            var content = await result.ToListAsync();

            List<object> list = new List<object>();
            foreach (var item in content)
            {
                list.Add(item);
            }

            Debug.WriteLine("result pls work: " + content);
            Console.Write(content.ToArray());
            return list;
        }
        //Select entire user + all group ID's part of that user
        public async Task<List<object>> TestingGrabUserAndGroups(int input)
        {
            //grab user and its users
            /*
             * SQL Version:
             * select *
             * from(tblUser
             * inner join tblUserToGroup on tblUser.fldUserID = tblUserToGroup.fldUserID)
             * inner join tblGroup on tblUserToGroup.fldGroupID = tblGroup.fldGroupID
             * where tblGroup.fldGroupID = '1';
             */

            var result = from u in TblUsers
                         where u.fldUserID == input
                         join gtu in TblUserToGroups on u.fldUserID equals gtu.fldUserID
                         join gtut in TblGroups on gtu.fldGroupID equals gtut.fldGroupID
                         select new
                         {
                             gtu.fldUserID,
                             gtut.fldGroupID,
                             gtut.fldGroupName,
                             gtut.fldGroupBoolean

                         };
            Debug.WriteLine("Result2: " + result);

            var content = await result.ToListAsync();
            Debug.WriteLine("Result4: " + content);
            List<object> list = new List<object>();
            foreach (var item in content)
            {
                list.Add(item);
            }

            Debug.WriteLine("result pls work: " + content);
            Console.Write(content);
            return list;
        }
        #endregion
        #region Testing - Updating Tables(EDIT)
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
            return utg.FldGroupId = group.FldGroupId;
            //GetAllTblGroups();
            //utg.FldGroupId = GetAllTblGroups().Id;
            //return Task.CompletedTask;
        }
        #endregion

        #region Add to Table(POST) - DONE


        public object RegisterUser(tblUser user)
        {
            var address = _httpClient.BaseAddress = new Uri("https://localhost:7002/TblUsers");
            //string url = address + group + "/" + ID;

            using (HttpClient client = new HttpClient())
            {
                //client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                List<KeyValuePair<string, string>> keyValues = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(user.fldEmail,"d@d.com"),
                    new KeyValuePair<string, string>(user.fldFirstName, "Michal"),
                    new KeyValuePair<string, string>(user.fldLastName, "Hein"),
                    new KeyValuePair<string, string>("fldPhonenumber", "10101010"),
                    new KeyValuePair<string, string>("fldIsAdmin", "true")
                };

                HttpContent content = new FormUrlEncodedContent(keyValues);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                content.Headers.ContentType.CharSet = "utf-8";

                var result = client.PostAsync(address, content).Result;
                Debug.WriteLine("result: " + result);
                string resultContent = result.Content.ReadAsStringAsync().Result;
                Debug.WriteLine("resultcontent: " + resultContent);
                return resultContent;
            }
        }

        [HttpPost]
        public object AddGroup(tblGroup group)
        {
            string name = group.fldGroupName = "";
            bool boll = (bool)(group.fldGroupBoolean = true);
            //string name, bool boll
            var result = PostHttpNewGroup("https://localhost:7002/TblGroups", name, boll);
            result.Wait();
            var content = result.Result;
            //string resultContent = result.ContinueWith().Result;
            Debug.WriteLine("result: " + content);
            return content;
        }
        [HttpPost]
        public object AddUser(TblUser user)
        {
            string mail = user.FldEmail;
            string fName = user.FldFirstName;
            string lName = user.FldLastName;
            int phone = user.FldPhonenumber;
            bool admin = user.FldIsAdmin;

            //string mail, string firstName, string lastName, int phone, bool admin
            var result = PostHttpNewUser("https://localhost:7002/TblUsers", mail, fName, lName, phone, admin);
            result.Wait();
            var content = result.Result;
            return content;
        }
        [HttpPost]
        public object AddLogin(TblLogin login)
        {
            int id = login.FldUserId;
            string pass = login.FldPassword;
            //int userID, string pass
            var result = PostHttpNewLogin("https://localhost:7002/TblLogins", id, pass);
            result.Wait();
            var content = result.Result;
            return content;
        }
        [HttpPost]
        public object AddReceipt(TblReceipt receipt)
        {
            int userID = receipt.FldUserId;
            int tripID = receipt.FldTripId;
            double value = receipt.FldProjectedValue;
            double paid = receipt.FldAmountPaid;
            //int userID, int tripID, double value, double paid
            var result = PostHttpNewReceipt("https://localhost:7002/TblReceipts", userID, tripID, value, paid);
            result.Wait();
            var content = result.Result;
            return content;
        }
        [HttpPost]
        public object AddTrip(TblTrip trip)
        {
            double sum = trip.FldSum;
            var result = PostHttpNewTrip("https://localhost:7002/TblTrips", sum);
            result.Wait();
            var content = result.Result;
            return content;
        }
        [HttpPost]
        public object AddUserExpense(TblUserExpense userExpense)
        {
            int userID = userExpense.FldUserId;
            double expense = userExpense.FldExpense;
            string note = userExpense.FldNote;
            DateTime date = userExpense.FldDate;
            //int userID, double expense, string note, DateTime date
            var result = PostHttpNewUserExpense("https://localhost:7002/TblUserExpensess", userID, expense, note, date);
            result.Wait();
            var content = result.Result;
            return content;
        }
        #endregion

        #region Delete data from Table (DELETE) - DONE
        [HttpDelete]
        public HttpResponseMessage ClientDeleteRequest(string group, int ID)
        {
            var address = _httpClient.BaseAddress = new Uri("https://localhost:7002/");
            string res = address + group + "/" + ID;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = client.DeleteAsync(res).Result;
            response.EnsureSuccessStatusCode();

            return response;
        }
        #endregion

        #region Get All from a Table
        public async Task<List<object>> TestGET()
        {
            List<tblGroup> model = null;
            var client = new HttpClient();

            var task = await client.GetAsync("https://localhost:7002/TblGroups");
            var jsonString = await task.Content.ReadAsStringAsync();
            model = JsonConvert.DeserializeObject<List<tblGroup>>(jsonString);
            Debug.WriteLine("BLABLABLA: "+model);
            return (List<object>)model.AsEnumerable();

        }
        public async Task<Task<tblGroup>> TestGETTWO()
        {
            using ( var client = new HttpClient() ) 
            {
                var apiUrl = "https://localhost:7002/TblGroups";
                var response = client.SendAsync(new HttpRequestMessage(HttpMethod.Get, apiUrl)).Result;
                if (!response.IsSuccessStatusCode)
                    return Task.FromCanceled<tblGroup>(new CancellationToken(true));
                var content = response.Content.ReadAsStringAsync().Result;
                return (Task<tblGroup>)await Task.FromResult(JsonConvert.DeserializeObject(content, typeof(tblGroup)));
            }
        }
        [HttpGet]
        public async Task<string> GetAllHttpResponse(string group)
        {
            //Create a Base Address
            var address = _httpClient.BaseAddress = new Uri("https://localhost:7002/");
            //Combine address with the selected group and id
            string res = address + group + "/";
            ///Debug.WriteLine("url: " + url);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(res).Result;
            response.EnsureSuccessStatusCode();
            ///Debug.WriteLine("response: "+response);

            //Makes the response into HTTP through serialization
            var content = await response.Content.ReadAsStringAsync();
            ///Debug.WriteLine("Final: " + content);
            return content;
        }

        [HttpGet]
        public async Task<object> GetGroupAsync(string path)
        {
            var address = _httpClient.BaseAddress = new Uri("https://localhost:7002/");
            string res = address + path+"/";
            Buffer.Clear();

            Buffer.Add(path);
            

            tblGroup group = null;
            HttpResponseMessage response = await _httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                group = await response.Content.ReadAsAsync<tblGroup>();
            }
            Debug.WriteLine("group: " + group);
            return group;
        }
        #endregion

        #region Get specific from table
        [HttpGet]
        public async Task<string> GetHttpResponse(string group, int ID)
        {
            //Create a Base Address
            var address = _httpClient.BaseAddress = new Uri("https://localhost:7002/");
            string url = address + group + "/" + ID;

           // Debug.WriteLine("url: " + url);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
           // Debug.WriteLine("response: " + response);

            //Makes the response into HTTP through serialization
           // var dub = JsonConvert.DeserializeObject<res>(await response.Content.ReadAsStringAsync());
            var content = await response.Content.ReadAsStringAsync();
           // Debug.WriteLine("Final2: " + content);
            return content;
        }

        #endregion
        #region Get Specific with linked properties (ex: 1 group and all users connected to it) - Not tested
        //GET with group id, grab that specific group + tblUsers inner join that matches group ID.
        //So when groupID is called, it displays that group + all users connected to that group,which it knows from inner join
        //and other way around

   
       

        #endregion

        #region Post new entry to a Table - WORKS, can be prettified

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

        #region Put/Replace data with new data - Not Tested
        [HttpPut]
        public async Task UpdateUser(int id, tblUser user)
        {
            string mail = user.fldEmail;
            string fName = user.fldFirstName;
            string lName = user.fldLastName;
            int phone = (int)user.fldPhonenumber;
            bool admin = (bool)user.fldIsAdmin;

            var handler = GetHttpResponse("TblUsers", id);
            var updatedUser = new tblUser
            {
                fldEmail = mail,
                fldFirstName = fName,
                fldLastName = lName,
                fldPhonenumber = phone,
                fldIsAdmin = admin
            };
            var editedUser = JsonSerializer.Serialize(updatedUser);
            var requestContent = new StringContent(editedUser, Encoding.UTF8, "application/json");
            //var uri = Path.Combine();
            var response = await _httpClient.PutAsync(await handler, requestContent);
            response.EnsureSuccessStatusCode();

        }
        #endregion

    }
}
