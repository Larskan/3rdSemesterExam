using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client
{
	/// <summary>
	/// The different types of log states
	/// </summary>
	public enum LogType
	{
		FatalError = 999,
		Warning = 500,
		API = 400,
		UserAction = 300,
		Information = 200,
		Success = 100,
	}

	/// <summary>
	/// The different types of parameters
	/// </summary>
	public enum ParameterType
	{
		String,
		Int32,
		Boolean
	}

	/// <summary>
	/// The different types of sql objects
	/// </summary>
	public enum SqlObjectType
	{
		tblGroup,
		tblGroupToMoney,
		tblReceipt,
		tblTrip,
		tblTripToUserExpense,
		tblUser,
		tblUserExpense,
		tblUserToGroup
	}

	/// <summary>
	/// The different types of api calls
	/// </summary>
	public enum APIMethod
	{
		Get,
		GetAll,
		Post,
		Put,
		Delete,
	}

}
