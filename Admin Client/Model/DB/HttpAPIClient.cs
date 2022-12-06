﻿using Admin_Client.Model.DB.EF_Test;
using DocumentFormat.OpenXml.Bibliography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.DB
{
	/// <summary>
	/// Used for communcation between AdminClient and the API
	/// </summary>
	public class HttpAPIClient
	{

		#region Variables

		// HttpClient
		private static HttpClient client = new HttpClient();

		#endregion

		#region Getters

		/// <summary>
		/// 
		/// |
		/// <para>
		/// How To Use:
		/// </para>
		/// 
		/// <para>
		/// <br></br>
		/// <br>-----\</br>
		/// <br></br>
		/// <br>Task&lt;object&gt;to = new HttpAPIClient().GetById(SqlObjectType.tblUser,1);</br>
		/// <br>await to;</br>
		/// <br>tblUser u = ((JObject)to.Result).ToObject&lt;tblUser&gt;();</br>
		/// <br></br>
		/// <br>-----/</br>
		/// <br></br>
		/// </para>
		/// 
		/// </summary>
		/// <param name="type">The type of Sql database you want to target</param>
		/// <param name="fldID">The selected objects id</param>
		/// <returns>A task with the selected object as result</returns>
		public async Task<object> GetById(SqlObjectType type, int fldID)
		{
			BaseSetup(client);

			//GET Method
			HttpResponseMessage response = await client.GetAsync(type +"s/" + fldID);

			if (response.IsSuccessStatusCode)
			{
				object o = await response.Content.ReadAsAsync<object>();
				return o;
			}
			else
			{
				Debug.WriteLine("Internal server Error");
				return null;
			}
		}

		/// <summary>
		/// 
		/// |
		/// <para>
		/// How To Use:
		/// </para>
		/// 
		/// <para>
		/// <br></br>
		/// <br>-----\</br>
		/// <br></br>
		/// <br>List&lt;tblUser&gt; users = new List&lt;tblUser&gt;();</br>
		/// <br></br>
		/// <br>Task&lt;List&lt;object&gt;&gt; to = new HttpAPIClient().GetAll(SqlObjectType.tblUser);</br>
		/// <br>await to;</br>
		/// <br>foreach (var item in to.Result)</br>
		/// <br></br>
		/// <br></br>
		/// <br>tblUser u = ((JObject)item).ToObject&lt;tblUser&gt;();</br>
		/// <br>users.Add(u);</br>
		/// <br></br>
		/// <br>-----/</br>
		/// <br></br>
		/// </para>
		/// 
		/// </summary>
		/// <param name="type">The type of Sql database you want to target</param>
		/// <returns>A task with the selected objects in a list as result</returns>
		public async Task<List<object>> GetAll(SqlObjectType type)
		{
				BaseSetup(client);

				//GETALL Method
				HttpResponseMessage response = await client.GetAsync(type + "s");

				if (response.IsSuccessStatusCode)
				{
					List<object> o = await response.Content.ReadAsAsync<List<object>>();
					return o;
				}
				else
				{
					Debug.WriteLine("Internal server Error");
					return null;
				}
		}

		#endregion

		#region Setters

		/// <summary>
		/// 
		/// |
		/// <para>
		/// How To Use:
		/// </para>
		/// 
		/// <para>
		/// <br></br>
		/// <br>-----\</br>
		/// <br></br>
		/// <br>new HttpAPIClient().Post(new tblUser() { fldEmail = "Test@smth.dk", fldFirstName = "Patrick", fldLastName = "Schemel", fldIsAdmin = true, fldPhonenumber = 42424242});</br>
		/// <br></br>
		/// <br>-----/</br>
		/// <br>Or</br>
		/// <br>-----\</br>
		/// <br></br>
		/// <br>new HttpAPIClient().Post(user);</br>
		/// <br></br>
		/// <br>-----/</br>
		/// <br></br>
		/// </para>
		/// 
		/// </summary>
		/// <param name="newObject">The object which will be added to the database</param>
		public async void Post(object newObject)
		{
			BaseSetup(client);

			//POST Method
			HttpResponseMessage response = await client.PostAsJsonAsync(newObject.GetType().Name + "s", newObject);

			if (response.IsSuccessStatusCode)
			{
				Debug.WriteLine(response.Headers.Location);
			}
			else
			{
				Debug.WriteLine("Internal server Error");
			}
		}

		/// <summary>
		/// 
		/// |
		/// <para>
		/// How To Use:
		/// </para>
		/// 
		/// <para>
		/// <br></br>
		/// <br>-----\</br>
		/// <br></br>
		/// <br>new HttpAPIClient().Put(new tblUser() { fldUserID = 1, fldEmail = "Changed@Person.dk", fldFirstName = "Patrick", fldLastName = "Schemel", fldIsAdmin = true, fldPhonenumber = 42424242 }, 1);</br>
		/// <br></br>
		/// <br>-----/</br>
		/// <br>Or</br>
		/// <br>-----\</br>
		/// <br></br>
		/// <br>new HttpAPIClient().Put(user, user.fldUserID);</br>
		/// <br></br>
		/// <br>-----/</br>
		/// <br></br>
		/// </para>
		/// 
		/// </summary>
		/// <param name="newObject">The object which will be the replacement of the targetet object in the database</param>
		/// <param name="fldID">The id of the targeted object</param>
		public async void Put(object newObject, int fldID)
		{
			BaseSetup(client);

			//PUT Method
			HttpResponseMessage response = await client.PutAsJsonAsync("tblUser" + "s/" + fldID, newObject);

			if (response.IsSuccessStatusCode)
			{
				Debug.WriteLine(response.Headers.Location);
			}
			else
			{
				Debug.WriteLine("Internal server Error");
			}
		}

		#endregion

		#region Delete

		/// <summary>
		/// 
		/// |
		/// <para>
		/// How To Use:
		/// </para>
		/// 
		/// <para>
		/// <br></br>
		/// <br>-----\</br>
		/// <br></br>
		/// <br>new HttpAPIClient().Delete(SqlObjectType.tblUser, 1);</br>
		/// <br></br>
		/// <br>-----/</br>
		/// <br></br>
		/// </para>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <param name="fldId"></param>
		public async void Delete(SqlObjectType type, int fldId)
		{
			BaseSetup(client);

			//DELETE Method
			HttpResponseMessage response = await client.DeleteAsync(type + "s/" + fldId);

			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("Deleted Successfully");
			}
			else
			{
				Debug.WriteLine("Internal server Error");
			}
		}

		#endregion

		#region BaseSetup

		/// <summary>
		/// Used to setup the base information for the client
		/// </summary>
		/// <param name="client"></param>
		private async void BaseSetup(HttpClient client)
		{
			client.BaseAddress = new Uri("https://localhost:7002/");
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		#endregion



	}
}
