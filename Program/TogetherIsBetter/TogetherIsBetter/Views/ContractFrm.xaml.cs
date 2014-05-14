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
using TogetherIsBetter.Model;

namespace TogetherIsBetter.Views
{
    /// <summary>
    /// Interaction logic for ContractFrm.xaml
    /// </summary>
    public partial class ContractFrm : Window
    {

        public Contract contract;

        public ContractFrm(Contract contract)
        {
            InitializeComponent();
            this.contract = contract;

            tboNumber.Text = contract.Number.ToString();
            dateStart.SelectedDate = contract.StartDate;
            dateEnd.SelectedDate = contract.EndDate;

            cboCompany.DataContext = Global.companies;
            cboContractFormula.DataContext = Global.contractFormula;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Hide();
        }
    }
}
