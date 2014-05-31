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
using TogetherIsBetter.Model;

namespace TogetherIsBetter.Forms
{
    /// <summary>
    /// Interaction logic for RegisterFrm.xaml
    /// </summary>
    public partial class ProfileFrm : Window
    {
        public MembershipUser user;
        public Company company;

        public String username;
        public String password;

        private bool isNewUser;

        public ProfileFrm(bool isNewUser, Company company, MembershipUser user = null)
        {
            InitializeComponent();
            this.user = user;
            this.company = company;
            this.isNewUser = isNewUser;

            // set combobox binding
            cboCompany.ItemsSource = Global.companies;
            cboCompany.DisplayMemberPath = "Name";
            cboCompany.SelectedValuePath = "id";

            // initial value
            cboCompany.SelectedItem = company;
            if (user != null)
                tbUsername.Text = user.UserName;


            if (isNewUser)
                tbPasswordOriginal.IsEnabled = false;
                        
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            String username = tbUsername.Text.Trim();
            String passwordOriginal = tbPasswordOriginal.Password.Trim();
            String passwordNew = tbPasswordNew.Password.Trim();
            String passwordConfirm = tbPasswordNewConfirm.Password.Trim();
            String errors = "";

            if (Val.isEmpty(username))
                errors += "Username can't be empty.";

            // if is new user or is existing user that has changed changed username
            if (isNewUser || (!isNewUser && username != user.UserName))
            {
                MembershipUser result = Membership.GetUser(username);
                if(result != null)
                    errors += "Username is already taken.";
            }

            if(Val.isLongerThan(username, 255))
                errors += "Username can't be longer than 255 characters.";
            if (!isNewUser && Val.isEmpty(passwordOriginal))
                errors += "Original password can't be empty.";
            if (Val.isEmpty(passwordNew))
                errors += "New password can't be empty.";
            if (Val.isEmpty(passwordConfirm))
                errors += "Confirm password can't be empty.";
            if (passwordNew != passwordConfirm)
                errors += "New password and confirm password must be the same.";

            if (errors != "")
            {
                errors = "Please fix this first: \n\n" + errors;
                MessageBox.Show(errors, "Incorrect input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!isNewUser && !user.ChangePassword(passwordOriginal, passwordNew))
            {
                MessageBox.Show("Original password is incorrect.", "Incorrect input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.username = username;
            this.password = passwordNew;

            int companyIndex = cboCompany.SelectedIndex;
            this.company = Global.companies[companyIndex];

            this.DialogResult = true;
            this.Hide();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }
    }
}
