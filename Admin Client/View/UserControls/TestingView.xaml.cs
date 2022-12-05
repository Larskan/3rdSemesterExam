﻿using Admin_Client.Model.DB.EF_Test;
using Admin_Client.Singleton;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            HttpClientServicesSingleton.Instance.TestingGrab("TblGroups", 1);
            //ResultList = HttpClientServicesSingleton.Instance.TestingGrabGroupAndUsers(1);
            
            /*
            Groups.Add(new tblGroup());
            var dub = HttpClientServicesSingleton.Instance.TestingGrabUserAndGroups(1).Result;
            foreach (var item in dub)
            {
                Groups.Add((tblGroup)item);
            }
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
        }

        private void ListRestult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
