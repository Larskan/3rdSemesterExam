using Admin_Client.Model.DB;
using Admin_Client.Model.Domain;
using Admin_Client.PropertyChanged;
using Admin_Client.Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Admin_Client.ViewModel.WindowModels.Popup
{
	public class PopupParameterChangeWindowModel : NotifyPropertyChangedHandler
	{

		#region Variables

		private Window currentWindow;

		private bool isNewObject = false;
		private object currentObject = false;
		private int currentObjectID = 0;

		#endregion

		#region Properties

		private string actionName = "";

		public string ActionName
		{
			get { return actionName; }
			set { actionName = value; }
		}


		private string objectName = "";

		public string ObjectName
		{
			get { return objectName; }
			set { objectName = value; }
		}

		private ObservableCollection<Parameter> parameters = new ObservableCollection<Parameter>();

		public ObservableCollection<Parameter> Parameters
		{
			get { return parameters; }
			set { parameters = value; }
		}

		#endregion

		#region Constructor

		public PopupParameterChangeWindowModel(Window currentWindow, object o)
		{
			this.currentWindow = currentWindow;
			this.currentObject = o;
			this.currentObjectID = (Int32)o.GetType().GetProperties().First().GetValue(o);

			foreach (var item in o.GetType().GetProperties())
			{
				string name = item.Name;
				string type = item.PropertyType.ToString().Replace("System.","");
				object value = null;

				switch (type)
				{
					case "String": value = (String)item.GetValue(o); break; 
					case "Int32": value = (Int32)item.GetValue(o); break;
					case "Boolean": value = (Boolean)item.GetValue(o); break;
					case "DateTime": value = (DateTime)item.GetValue(o); break;
				}
				if (name.ToLower().Contains("name"))
				{
					// If the object has two or more parts to their name
					if (ObjectName.Length > 0)
					{
						ObjectName += " ";
					}
					if (value != null)
					{
						ObjectName += value.ToString();
					}
				}
				if (!name.Contains("ID") && !name.ToLower().Contains("password") && Enum.GetNames(typeof(ParameterType)).Contains(type))
				{
					if (value != null)
					{
						Parameters.Add(new Parameter(name, (ParameterType)Enum.Parse(typeof(ParameterType), type), value.ToString()));
					} else
					{
						Parameters.Add(new Parameter(name, (ParameterType)Enum.Parse(typeof(ParameterType), type), ""));
						if (!isNewObject)
						{
							isNewObject = true;
						}
					}
				}
			}
			if (isNewObject)
			{
				this.ActionName = "Create New";
				this.ObjectName = o.GetType().Name;
			} else
			{
				this.ActionName = "Add Parameters For";
			}
		}

		#endregion

		#region Public Methods

		public void Confirm(ListBox listBox)
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Add Click"));

			bool isValid;
			foreach (var item in listBox.Items)
			{
				isValid = false;

				// DATATYPE VALID
				switch (((Parameter)item).ParameterType)
				{
					case ParameterType.String:
						{
							try
							{
								((Parameter)item).ParameterValue.ToString();
								isValid= true;
							} catch { }
							break;
						}
					case ParameterType.Int32:
						{
							try
							{
								Int32.Parse(((Parameter)item).ParameterValue.ToString());
								isValid = true;
							}
							catch { }
							break;
						}
					case ParameterType.Boolean:
						{
							try
							{
								Boolean.Parse(((Parameter)item).ParameterValue.ToString());
								isValid = true;
							}
							catch { }
							break;
						}
				}

				// EMAIL NOT-VALID
				if (((Parameter)item).ParameterName.ToLower().Contains("email"))
				{
					Regex regex = new Regex(@"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*))");
					if (!regex.IsMatch(((string)((Parameter)item).ParameterValue)))
					{
						isValid = false;
					}
				}
				// PHONENUMBER NOT-VALID
				if (((Parameter)item).ParameterName.ToLower().Contains("phonenumber"))
				{
					if (((string)((Parameter)item).ParameterValue).Length != 8)
					{
						isValid = false;
					}
				}

				// Valid or noy
				if (!isValid)
				{	
					((Parameter)item).IsValid = false;
				} else
				{
					((Parameter)item).IsValid = true;
				}
			}

			// Check if everything is valid
			foreach (var item in listBox.Items)
			{
				if (!((Parameter)item).IsValid)
				{
					return;
				}
			}

			// Make changes to object
			foreach (var parameterProperty in listBox.Items)
			{
				foreach (var objectProperty in currentObject.GetType().GetProperties())
				{
					if (((Parameter)parameterProperty).ParameterName.Equals(objectProperty.Name))
					{
						switch (((Parameter)parameterProperty).ParameterType)
						{
							case ParameterType.String:
								{
									currentObject.GetType().GetProperty(objectProperty.Name).SetValue(currentObject, ((Parameter)parameterProperty).ParameterValue);
									break;
								}
							case ParameterType.Int32:
								{
									currentObject.GetType().GetProperty(objectProperty.Name).SetValue(currentObject, Int32.Parse(((Parameter)parameterProperty).ParameterValue.ToString()));
									break;
								}
							case ParameterType.Boolean:
								{
									currentObject.GetType().GetProperty(objectProperty.Name).SetValue(currentObject, Boolean.Parse(((Parameter)parameterProperty).ParameterValue.ToString()));
									break;
								}
						}
					}
				}
			}

			// Send changed object to the database
			if (isNewObject)
			{
				HttpClientHandler.Post(currentObject);
			} else
			{
				HttpClientHandler.Put(currentObject, currentObjectID);
			}

			currentWindow.Close();
			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = true;
		}

		public void Cancel()
		{
			LogHandlerSingleton.Instance.WriteToLogFile(new Log(LogType.UserAction, "Cancel Click"));
			currentWindow.Close();
			MainWindowModelSingleton.Instance.GetMainWindow().IsEnabled = true;
		}

		#endregion

	}
}
