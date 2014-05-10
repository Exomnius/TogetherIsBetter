using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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

            LoginForm loginFrm = new LoginForm(user);
            bool result = (bool)loginFrm.ShowDialog();            

            if (result)
            {
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
