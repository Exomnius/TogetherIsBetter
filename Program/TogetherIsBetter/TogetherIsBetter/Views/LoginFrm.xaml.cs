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
        User user;

        public LoginForm(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Add default user 
            MembershipUserCollection collection = Membership.FindUsersByName("admin");

            if (collection.Count == 0)
            {
                Membership.CreateUser("admin", "admin!");

                if (!Roles.RoleExists("admin"))
                    Roles.CreateRole("admin");
                if (Roles.IsUserInRole("admin", "admin"))
                    Roles.AddUserToRole("admin", "admin");
            } 
           
            
            String username, password;

            username = tbUsername.Text;
            password = tbPassword.Password;

            if (Membership.ValidateUser(username, password))
            {
                user.Authenticated = true;
                user.Username = username;

                if (Roles.IsUserInRole(username, "admin"))
                    user.Role = "admin";

                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Ongeldige gebruikersnaam en/of wachtwoord.");
                user.Authenticated = false;
            }


        }

        private void Login_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
