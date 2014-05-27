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

            setDefaultUsers();
            
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

        // Add default user and admin
        private void setDefaultUsers()
        {
            
            MembershipUserCollection collection = Membership.FindUsersByName("admin");

            if (collection.Count == 0)
            {
                Membership.CreateUser("admin", "admin!");

                if (!Roles.RoleExists("admin"))
                    Roles.CreateRole("admin");
                if (!Roles.IsUserInRole("admin", "admin"))
                    Roles.AddUserToRole("admin", "admin");
            }

            collection = Membership.FindUsersByName("user");

            if (collection.Count == 0)
            {
                Membership.CreateUser("user", "user!");

                if (!Roles.RoleExists("user"))
                    Roles.CreateRole("user");
                if (!Roles.IsUserInRole("user", "user"))
                    Roles.AddUserToRole("user", "user");
            } 
        }

        private void Login_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
