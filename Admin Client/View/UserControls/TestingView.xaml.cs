using Admin_Client.Model.DB;
using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Singleton;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Admin_Client.View.UserControls
{
    /// <summary>
    /// Interaction logic for TestingView.xaml
    /// </summary>
    public partial class TestingView : Window
    {
        private ObservableCollection<tblGroup> groups = new ObservableCollection<tblGroup>();
        public ObservableCollection<tblGroup> Groups
        {
            get { return groups; }
            set { groups = value; }
        }


        public TestingView()
        {
            this.DataContext= this;
            InitializeComponent();
            
        }

        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
           

            /*
            var testObject = new tblGroup()
            {
                fldGroupName = "Hello",
                fldGroupBoolean = true,
                Friends = new List<TestSubObject>()
                {
                    new TestSubObject()
                    {
                        Id= 1,
                        Name = "TestSubName",
                    }
                }
            };
            

            var content = HttpContentBody.GetHttpContent(testObject);
            var contentString = await content.ReadAsStringAsync();

            Debug.WriteLine("TEST: "+contentString);
            */

            //_ = HttpClientServicesSingleton.Instance.GetHttpResponse("tblGroup", 1);
            //HttpClientServicesSingleton.Instance.RegisterUser(new tblUser());
            //HttpClientServicesSingleton.Instance.AddGroup(new tblGroup());
            // var dun = HttpClientServicesSingleton.Instance.GetGroupAsync("https://localhost:7002/TblGroups".Normalize());
            // foreach (var item in dun)
            // {
            //     Groups.Add((tblGroup)item);
            //     dun.
            //  }
            //  while(dun != null)
            //  {
            //      Groups.Add((tblGroup)dun);
            // }
            //var dubn = HttpClientServicesSingleton.Instance.TestGET();
            //var dub = HttpClientServicesSingleton.Instance.TestGETTWO();
            // TextResult.Text = dub.Result;

            var sup = HttpClientServicesSingleton.Instance.Get(1);
            Debug.WriteLine("Sup: " + sup);
        }

       
    }
}
