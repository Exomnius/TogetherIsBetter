using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TogetherIsBetter.Model;

namespace TogetherIsBetter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            User user = new User();

            user.Role = "admin";
            user.Username = "admin";
            ////user.Role = "user";
            ////user.Username = "user";
            bool result = true;

            //LoginForm loginFrm = new LoginForm(user);
            //bool result = (bool)loginFrm.ShowDialog();            

            if (result)
            {
                // init global vars
                //Global.companies = Companies.getCompanies();
                Generic<Company> generic = new Generic<Company>();
                Global.companies = generic.GetAll().ToList();
                generic.Dispose();
                

                // show main window
                MainWindow appmainwindow = new MainWindow(user);
                Application.Current.MainWindow = appmainwindow;
                appmainwindow.Activate();
                appmainwindow.Show();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
    }
}
