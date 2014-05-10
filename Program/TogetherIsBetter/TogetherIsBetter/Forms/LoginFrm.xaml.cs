using System;
using System.Collections.Generic;
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
using System.Web.Security;

namespace TogetherIsBetter
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            String username, password;

            username = tbUsername.Text;
            password = tbPassword.Password;

            if (Membership.ValidateUser(username, password))
            {
                MessageBox.Show("GELDIG!");
            }
            else
            {
                MessageBox.Show("Ongeldige gebruikersnaam en/of wachtwoord.");
            }


        }
    }
}
