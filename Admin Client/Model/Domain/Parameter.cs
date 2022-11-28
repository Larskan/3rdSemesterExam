using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin_Client.Model.Domain
{
	/// <summary>
	/// Used to store parameter information
	/// </summary>
	public class Parameter
	{
		public string ParameterName { get; set; }
		public string ParameterTypeString { get; set; }
		public ParameterType ParameterType { get; set; }
		public object ParameterValue { get; set; }

		/// <summary>
		/// Create a new ParameterContainer
		/// </summary>
		/// <param name="name">The name of the property</param>
		/// <param name="type">The type of the property</param>
		/// <param name="value">The value of the property</param>
		public Parameter(string name, ParameterType type, object value) 
		{ 
			this.ParameterName = name;
			this.ParameterType = type;
			this.ParameterTypeString = this.ParameterType.ToString();
			this.ParameterValue = value;
		}

	}
}
