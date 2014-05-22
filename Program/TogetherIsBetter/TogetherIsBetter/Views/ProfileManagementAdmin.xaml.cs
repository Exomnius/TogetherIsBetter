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

namespace TogetherIsBetter.Views
{
    /// <summary>
    /// Interaction logic for ProfileManagementAdmin.xaml
    /// </summary>
    public partial class ProfileManagementAdmin : Window
    {
        public ProfileManagementAdmin()
        {
            InitializeComponent();

            MembershipUserCollection allUsers = Membership.GetAllUsers();
            
            foreach(MembershipUser user in allUsers){
                lbUsers.Items.Add(user.UserName);
            }
        }

        private void btnNewContract_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditContract_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteContract_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
