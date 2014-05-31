using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TogetherIsBetter.Forms;
using TogetherIsBetter.Model;

namespace TogetherIsBetter.Views
{
    /// <summary>
    /// Interaction logic for ProfileManagementAdmin.xaml
    /// </summary>
    public partial class ProfileAdminFrm : Window
    {

        List<UsersPerCompany> usersPerCompany;

        public ProfileAdminFrm()
        {
            InitializeComponent();

            // set combobox binding
            cboCompany.ItemsSource = Global.companies;
            cboCompany.DisplayMemberPath = "Name";
            cboCompany.SelectedValuePath = "id";
            cboCompany.SelectedIndex = 0;

            if (!Global.user.IsAdmin)
                cboCompany.IsEnabled = false;

            getUsersForSelectedCompany();
        }

        public void getUsersForSelectedCompany()
        {
            // clear all items
            lbUsers.Items.Clear();

            // fetch usersPerCompany
            Generic<UsersPerCompany> generic = new Generic<UsersPerCompany>();
            usersPerCompany = generic.GetAll().ToList();
            generic.Dispose();

            Company selectedCompany = getSelectedCompany();
            if (selectedCompany == null) return;

            //fill listbox with usernames of users from selected company
            foreach (UsersPerCompany user in usersPerCompany)
            {
                if (user.CompanyId == selectedCompany.Id)
                {
                    String username = Membership.GetUser(user.UserId).UserName;
                    lbUsers.Items.Add(username);
                }
            }
        }

        public Company getSelectedCompany(){
            
            int companyIndex = cboCompany.SelectedIndex;
            if (companyIndex == -1) return null;

            return Global.companies[companyIndex];
        }

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            createOrEditUser(newUser: true);
            getUsersForSelectedCompany();
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            createOrEditUser(newUser: false);
            getUsersForSelectedCompany();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedIndex == -1) return;

            if (MessageBoxResult.Yes == MessageBox.Show("Are you sure you want to delete this profile?", "Are you sure?", MessageBoxButton.YesNo))
            {
                try
                {
                    Company company = getSelectedCompany();

                    String username = (String)lbUsers.SelectedValue;
                    MembershipUser user = Membership.GetUser(username);
                    Membership.DeleteUser(username);

                    UsersPerCompany userCompany = new UsersPerCompany();
                    userCompany.CompanyId = company.Id;
                    userCompany.UserId = (Guid)user.ProviderUserKey;

                    Generic<UsersPerCompany> generic = new Generic<UsersPerCompany>();
                    generic.Delete(userCompany);
                    generic.Dispose();

                    MessageBox.Show("The profile was removed successfully", "Profile removed", MessageBoxButton.OK, MessageBoxImage.Information);
                    getUsersForSelectedCompany();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                    MessageBox.Show("There was a problem removing this profile from the database. Please try again later or contact a sysadmin.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void cboCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getUsersForSelectedCompany();
        }

        private void lbUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            createOrEditUser(newUser: false);
            getUsersForSelectedCompany();
        }

        public void createOrEditUser(bool newUser)
        {
            if (!newUser && lbUsers.SelectedIndex == -1) return;
            
            Company company = getSelectedCompany();
            ProfileFrm form = null;
            MembershipUser user = null;
            bool profileFrmResult = false;

            try
            {
                if (newUser)
                {
                    form = new ProfileFrm(true, company);
                    profileFrmResult = (bool)form.ShowDialog();
                    if (profileFrmResult)
                        user = Membership.CreateUser(form.username, form.password);
                } else {
                    String username = (String)lbUsers.SelectedValue;
                    user = Membership.GetUser(username);

                    form = new ProfileFrm(false, company, user);
                    profileFrmResult = (bool)form.ShowDialog();
                    if (profileFrmResult)
                        Membership.UpdateUser(user);
                }

                if (!profileFrmResult) return;

                UsersPerCompany newUserCompany = new UsersPerCompany();
                UsersPerCompany oldUserCompany = new UsersPerCompany();

                newUserCompany.CompanyId = form.company.Id;
                newUserCompany.UserId = (Guid)user.ProviderUserKey;

                oldUserCompany.CompanyId = company.Id;
                oldUserCompany.UserId = (Guid)user.ProviderUserKey;

                Generic<UsersPerCompany> generic = new Generic<UsersPerCompany>();
                if (!newUser)
                    generic.Delete(oldUserCompany);

                generic.Add(newUserCompany);    
                generic.Dispose();
                form.Close();
                MessageBox.Show("The profile was saved successfully", "Company saved", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                MessageBox.Show("There was a problem saving this profile to the database. Please try again later or contact a sysadmin.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }         
        }
    }
}
