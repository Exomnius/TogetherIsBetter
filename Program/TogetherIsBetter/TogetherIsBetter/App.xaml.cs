using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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

            startApp(debugMode: false);
            //startApp(debugMode: true);
      
            
        }

        public static void startApp(bool debugMode = false){

            if (debugMode)
            {
                Global.user = new User();
                Global.user.Role = "admin";
                Global.user.Username = "admin";
                // Global.user.Role = "user";
                // Global.user.Username = "user";
            }

            if (debugMode || showLoginFrm())
            {
                // user logged in
                // init global vars
                Generic<Company> generic = new Generic<Company>();
                Global.companies = generic.GetAll().ToList();
                generic.Dispose();

                // show main window
                MainWindow appmainwindow = new MainWindow(Global.user);
                appmainwindow.Activate();
                appmainwindow.ShowDialog();

                if (appmainwindow.logout) // user clicked logout
                {
                    Process.Start(Application.ResourceAssembly.Location); // starts new instance of program
                }
            }

            Application.Current.Shutdown();

        }


        public static bool showLoginFrm()
        {
            Global.user = new User();
            LoginForm loginFrm = new LoginForm(Global.user);

            bool result = (bool)loginFrm.ShowDialog();
            loginFrm.Close();
            
            return result;
        }
    }
}
