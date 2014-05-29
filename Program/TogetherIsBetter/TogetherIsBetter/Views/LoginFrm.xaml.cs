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
using TogetherIsBetter.Model;

namespace TogetherIsBetter
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        User user;
        public bool result;
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
                user.Membership = Membership.GetUser(username);
                result = true;

                if (Roles.IsUserInRole(username, "admin"))
                    user.Role = "admin";
                else
                    user.Role = "user";

                // get usersPerCompany
                Generic<UsersPerCompany> usersPerCompany = new Generic<UsersPerCompany>();
                UsersPerCompany userCompany = usersPerCompany.GetAll().ToList().Find(u => u.UserId == (Guid)user.Membership.ProviderUserKey);
                usersPerCompany.Dispose();

                if (userCompany != null)
                {
                    // get users company
                    Generic<Company> company = new Generic<Company>();
                    user.Company = company.Get(userCompany.CompanyId);
                    company.Dispose();
                }
                else
                {
                    // get default company - hardcoded
                    Generic<Company> company = new Generic<Company>();
                    user.Company = company.Get(1);
                    company.Dispose();
                }


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
                saveDefaultUsersCompany("admin");    

                if (!Roles.RoleExists("admin"))
                    Roles.CreateRole("admin");
                if (!Roles.IsUserInRole("admin", "admin"))
                    Roles.AddUserToRole("admin", "admin");
            }

            collection = Membership.FindUsersByName("user");

            if (collection.Count == 0)
            {
                Membership.CreateUser("user", "user!");
                saveDefaultUsersCompany("user");

                if (!Roles.RoleExists("user"))
                    Roles.CreateRole("user");
                if (!Roles.IsUserInRole("user", "user"))
                    Roles.AddUserToRole("user", "user");
            } 
        }

        private void saveDefaultUsersCompany(String username)
        {
            UsersPerCompany userCompany = new UsersPerCompany();
            userCompany.UserId = (Guid)Membership.GetUser(username).ProviderUserKey;
            userCompany.CompanyId = 1;

            Generic<UsersPerCompany> generic = new Generic<UsersPerCompany>();
            generic.Add(userCompany);
            generic.Dispose();
        }

        private void Login_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
