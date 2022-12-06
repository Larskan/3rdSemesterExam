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

namespace Admin_Client.Model.DB
{
	/*
     * Todo: Edit/Put/ReplaceRequest - Work in progress
     * Todo: If I grab a groupID I need to get foreign keys too(Users connected to a group)
     * Todo: Make User and Group communicate through tblUserToGroup 
     */
	public class HttpClientServices : HttpDbContext
	{
		//Having it static reduces waste sockets and makes it faster
		private static readonly HttpClient _httpClient = new HttpClient();



		public HttpClientServices()
		{
			_httpClient.BaseAddress = new Uri("https://localhost:7270/");
		}


		#region testing
		//TESTING METHOD FOR GETTING USERS AND GROUPS TO DISPLAY TOGETHER, depending on which ID is used
		[HttpGet("{id}")]
		public Task GetGroupAndUsers(int id)
		{
			tblGroup dot = new tblGroup();
			tblUser dut = new tblUser();

			//Can change to switch once it works

			if (id == dot.fldGroupID)
			{
				//Display group + all users part of that group (1 group has many users)
				_ = GetHttpResponse("tblUserToGroups", dot.fldGroupID);
				return Task.CompletedTask;
			}
			else if (id == dut.fldUserID)
			{
				//Display user + all groups that user is part of (1 user has many groups)
				_ = GetHttpResponse("tblUserToGroups", dut.fldUserID);
			}
			return Task.CompletedTask;
		}


		[HttpGet("{id}")]
		public Task InnerJoin(int id)
		{
			List<tblGroup> groups = new List<tblGroup>();
			List<tblUser> users = new List<tblUser>();
			List<tblUserToGroup> combined = new List<tblUserToGroup>();
			Debug.WriteLine("List groups: " + groups.Count);
			Debug.WriteLine("List users: " + users.Count);

			tblGroup dot = new tblGroup();
			tblUser dut = new tblUser();
			tblUserToGroup dit = new tblUserToGroup();


			if (id == dot.fldGroupID)
			{
				//Display group + all users part of that group (1 group has many users)
				_ = GetHttpResponse("tblUserToGroups", dot.fldGroupID);

				IEnumerable<tblGroup> matchID = (from tblGroup groupItemGroup in groups
												 join tblUser groupItemUser in users
												 on groupItemGroup.fldGroupID
												 equals groupItemUser.fldUserID
												 select groupItemGroup);


				/*
                var q = (from tg in tblGroups
                         join tutg in tblUserToGroups on tg.fldGroupID equals tutg.fldGroupID
                         join tu in tblUsers on tutg.fldUserID equals tu.fldUserID
                         orderby tutg.FldUserToGroupId
                         select new
                         {
                             tutg.FldUserToGroupID,
                             tg.fldGroupID,
                             tg.fldGroupName,
                             tu.fldUserID,
                         });
                */


				//OutputCollectionToConsole(matchID);
				//Console.ReadKey();

				return (Task)matchID;
			}
			else if (id == dut.fldUserID)
			{
				//Display user + all groups that user is part of (1 user has many groups)
				_ = GetHttpResponse("tblUserToGroups", dut.fldUserID);

				IEnumerable<tblUser> matchID = (from tblUser groupItemUser in users
												join tblGroup groupItemGroup in groups
												on groupItemUser.fldUserID
												equals groupItemGroup.fldGroupID
												select groupItemUser);
				OutputCollectionToConsole(matchID);
				Console.ReadKey();
				return (Task)matchID;
			}


			//else if(id == dut.fldUserID && dot.fldGroupID){  }
			//return (Task)matchID;
			return Task.CompletedTask;
		}

		private static void OutputCollectionToConsole(IEnumerable<object> collectionToOutput)
		{
			foreach (object collectionItem in collectionToOutput)
				Console.WriteLine(collectionItem.ToString());
		}
		#endregion

		#region Updating Tables
		private int UpdateUserID(tblUserToGroup utg)
		{
			tblUser user = new tblUser();
			//GetAlltblUsers();
			return utg.fldUserID = user.fldUserID;
			//return Task.CompletedTask;

		}
		private int UpdateGroupID(tblUserToGroup utg)
		{
			tblGroup group = new tblGroup();
			return utg.fldGroupID = group.fldGroupID;
			//GetAlltblGroups();
			//utg.fldGroupID = GetAlltblGroups().Id;
			//return Task.CompletedTask;
		}
		#endregion

		#region Add to Table

		[HttpPost]
		public Task AddGroup(tblGroup group)
		{
			string name = group.fldGroupName = "";
			bool boll = (bool)(group.fldGroupBoolean = true);
			//string name, bool boll
			_ = PostHttpNewGroup("https://localhost:7002/tblGroups", name, boll);
			return Task.CompletedTask;
		}
		[HttpPost]
		public Task AddUser(tblUser user)
		{
			string mail = user.fldEmail;
			string fName = user.fldFirstName;
			string lName = user.fldLastName;
			int? phone = user.fldPhonenumber;
			bool? admin = user.fldIsAdmin;

			//string mail, string firstName, string lastName, int phone, bool admin
			_ = PostHttpNewUser("https://localhost:7002/tblUsers", mail, fName, lName, phone, admin);
			return Task.CompletedTask;
		}
		[HttpPost]
		public Task AddLogin(tblLogin login)
		{
			int? id = login.fldUserID;
			string pass = login.fldPassword;
			//int userID, string pass
			_ = PostHttpNewLogin("https://localhost:7002/tblLogins", id, pass);
			return Task.CompletedTask;
		}
		[HttpPost]
		public Task AddReceipt(tblReceipt receipt)
		{
			int? userID = receipt.fldUserID;
			int? tripID = receipt.fldTripID;
			double? value = receipt.fldProjectedValue;
			double? paid = receipt.fldAmountPaid;
			//int userID, int tripID, double value, double paid
			_ = PostHttpNewReceipt("https://localhost:7002/tblReceipts", userID, tripID, value, paid);
			return Task.CompletedTask;
		}
		[HttpPost]
		public Task AddTrip(tblTrip trip)
		{
			double? sum = trip.fldSum;
			_ = PostHttpNewTrip("https://localhost:7002/tblTrips", sum);
			return Task.CompletedTask;
		}
		[HttpPost]
		public Task AddUserExpense(tblUserExpense userExpense)
		{
			int? userID = userExpense.fldUserID;
			double? expense = userExpense.fldExpense;
			string note = userExpense.fldNote;
			DateTime? date = userExpense.fldDate;
			//int userID, double expense, string note, DateTime date
			_ = PostHttpNewUserExpense("https://localhost:7002/tblUserExpensess", userID, expense, note, date);
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
			response.EnsureSuccessStatusCode();

			return response;
		}
		#endregion

		#region Get All from a Table
		[HttpGet]
		public async Task<string> GetAllHttpResponse(string group)
		{
			//Create a Base Address
			var address = _httpClient.BaseAddress = new Uri("https://localhost:7002/");
			//Combine address with the selected group and id
			string res = address + group + "/";
			///Debug.WriteLine("res: " + res);
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
		#endregion

		#region Get specific from table
		[HttpGet]
		public async Task<string> GetHttpResponse(string group, int ID)
		{
			//Create a Base Address
			var address = _httpClient.BaseAddress = new Uri("https://localhost:7002/");

			string res = address + group + "/" + ID;
			///Debug.WriteLine("res: " + res);
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			HttpResponseMessage response = client.GetAsync(res).Result;
			response.EnsureSuccessStatusCode();
			///Debug.WriteLine("response: " + response);

			//Makes the response into HTTP through serialization
			var content = await response.Content.ReadAsStringAsync();
			///Debug.WriteLine("Final2: " + content);
			return content;
		}

		#endregion
		#region Get Specific with linked properties (ex: 1 group and all users connected to it)
		//GET with group id, grab that specific group + tblUsers inner join that matches group ID.
		//So when groupID is called, it displays that group + all users connected to that group,which it knows from inner join
		//and other way around

		public async Task<string> TestingGrab(string group, int id)
		{
			tblUser tblUser = new tblUser();
			tblGroup tblGroup = new tblGroup();
			var handler = GetHttpResponse(group, id);
			var address = _httpClient.BaseAddress = new Uri("https://localhost:7002/");
			var res = address + group + "/" + id;
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var query1 = from fldGroupID in tblGroups select fldGroupID;
			var query2 = from fldUserID in tblUsers select fldUserID;
			var query3 = from fldGroupID in tblUserToGroups select fldGroupID;
			Debug.WriteLine("Query1" + query1);
			Debug.WriteLine("Query2" + query2);
			Debug.WriteLine("Query3" + query3);

			//it does the query1-3 but not innerjoins, exception thrown
			var innerJoinQuery =
				from fldGroupID in tblGroups
				join fldUserID in tblUsers on fldGroupID.tblUserToGroup equals fldUserID.tblUserToGroup
				select fldUserID;
			Debug.WriteLine("InnerJoinQuery: " + innerJoinQuery.ToString());
			var content = await innerJoinQuery.ToListAsync();
			Debug.WriteLine("Content: " + content);
			Debug.WriteLine("ContentToArray: " + content.ToArray());
			Debug.WriteLine("ContentToList: " + content.ToList());
			Debug.WriteLine("ContentToString: " + content.ToString());
			return content.ToString();
		}


		//Select entire group + all user ID's part of that group
		public async Task<string> TestingGrabGroupAndUsers(int input)
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

			var result = (from g in tblGroups
						  where g.fldGroupID.Equals(input)
						  join gtu in tblUserToGroups on g.fldGroupID
						  equals gtu.fldGroupID
						  join gtut in tblUsers on gtu.fldUserID equals gtut.fldUserID
						  select new
						  {
							  gtu.fldGroupID,
							  gtu.fldUserID
						  });
			Debug.WriteLine("Result1: " + result.ToString());
			Debug.WriteLine("Result1: " + result);
			var content = await result.ToListAsync();

			Debug.WriteLine("result pls work: " + content);
			Console.Write(content.ToArray());
			return content.ToString();
		}
		//Select entire user + all group ID's part of that user
		public async Task<string> TestingGrabUserAndGroups(int input)
		{
			//grab user and its groups
			/*
             * SQL Version:
             * select *
             * from(tblUser
             * inner join tblUserToGroup on tblUser.fldUserID = tblUserToGroup.fldUserID)
             * inner join tblGroup on tblUserToGroup.fldGroupID = tblGroup.fldGroupID
             * where tblGroup.fldGroupID = '1';
             */

			var result = (from u in tblUsers
						  where u.fldUserID == input
						  join gtu in tblUserToGroups on u.fldUserID
						  equals gtu.fldUserID
						  join gtut in tblGroups on gtu.fldGroupID equals gtut.fldGroupID
						  select new
						  {
							  gtu.fldUserID,
							  gtu.fldGroupID
						  });
			Debug.WriteLine("Result2: " + result.ToString());
			Debug.WriteLine("Result2: " + result);

			var content = await result.ToListAsync();

			Debug.WriteLine("result pls work: " + content);
			Console.Write(content);
			return content.ToString();
		}

		#endregion

		#region Post new entry to a Table

		[HttpPost]
		public async Task<string> PostHttpNewGroup(string url, string name, bool groupBool)
		{
			var endpoint = _httpClient.BaseAddress = new Uri(url);
			var newPost = new tblGroup()
			{
				fldGroupName = name,
				fldGroupBoolean = groupBool
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
		public async Task<string> PostHttpNewUser(string url, string mail, string firstName, string lastName, int? phone, bool? admin)
		{
			var endpoint = _httpClient.BaseAddress = new Uri(url);
			var newPost = new tblUser()
			{
				fldEmail = mail,
				fldFirstName = firstName,
				fldLastName = lastName,
				fldPhonenumber = phone,
				fldIsAdmin = admin
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
		public async Task<string> PostHttpNewLogin(string url, int? userID, string pass)
		{
			var endpoint = _httpClient.BaseAddress = new Uri(url);
			var newPost = new tblLogin()
			{
				fldUserID = userID,
				fldPassword = pass
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
		public async Task<string> PostHttpNewReceipt(string url, int? userID, int? tripID, double? value, double? paid)
		{
			var endpoint = _httpClient.BaseAddress = new Uri(url);
			var newPost = new tblReceipt()
			{
				fldUserID = userID,
				fldTripID = tripID,
				fldProjectedValue = value,
				fldAmountPaid = paid
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
		public async Task<string> PostHttpNewTrip(string url, double? sum)
		{
			var endpoint = _httpClient.BaseAddress = new Uri(url);
			var newPost = new tblTrip()
			{
				fldSum = sum
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
		public async Task<string> PostHttpNewUserExpense(string url, int? userID, double? expense, string note, DateTime? date)
		{
			var endpoint = _httpClient.BaseAddress = new Uri(url);
			var newPost = new tblUserExpense()
			{
				fldUserID = userID,
				fldExpense = expense,
				fldNote = note,
				fldDate = date
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
		[HttpPut]
		public async Task<string> ReplaceContentHttp(string group, int ID)
		{
			//Grab Group and specific ID inside group - DONE
			//Load what group contains - DONE
			//Pick something that isnt primary key to change
			//update database wíth new data
			string container = "";
			using (HttpClient client = new HttpClient())
			{
				var address = _httpClient.BaseAddress = new Uri("https://localhost:7002/");
				string res = address + group + "/" + ID;
				Debug.WriteLine("Res: " + res);

				var grab = await GetHttpResponse(group, ID);
				grab.AsQueryable().FirstOrDefault();
				switch (grab)
				{
					case "tblGroups":
						tblGroup tblGroup = new tblGroup();
						if (ID != tblGroup.fldGroupID)
						{
							break;
						}


						break;
					case "tblUsers":
						//
						break;
					case "tblLogins":
						//
						break;
					case "tblReceipts":
						//
						break;
					case "tblTrips":
						//
						break;
					case "tblUserExpensess":
						//
						break;
				}

				//Works fine, but container is empty, need to find a way to insert the new parameters
				//var json = JsonConvert.SerializeObject();
				//var stringContent = new StringContent(json,UnicodeEncoding.UTF8, "application/json");
				//var respo = await client.PutAsync(res, stringContent);
				//client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				var content = new FormUrlEncodedContent(new[]
				{
					new KeyValuePair<string, string>("fldGroupName","replaceTest2")
				});
				Debug.WriteLine("Content: " + content);

				var result = await client.PutAsync(res, content);
				Debug.WriteLine("Result: " + result);
				container = await result.Content.ReadAsStringAsync();
				Debug.WriteLine("Container: " + container);
			}
			return container;
		}
		#endregion





	}
}
