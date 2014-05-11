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
    /// Interaction logic for ContractManagementFrm.xaml
    /// </summary>
    public partial class ContractManagementFrm : Window
    {
        public ContractManagementFrm()
        {
            InitializeComponent();
            loadContracts();
            loadContractFormula();
        }

        private void loadContracts()
        {
            Global.contracts = Contracts.getContracts();

            lbContracts.Items.Clear();

            foreach (Contract contract in Global.contracts)
            {
                lbContracts.Items.Add(String.Format("{0} \t({1:dd/MM/yy} - {2:dd/MM/yy})", Global.companies.Find(c => c.Id == contract.CompanyId).Name, contract.StartDate, contract.EndDate));
            }
        }

        private void loadContractFormula()
        {
            Global.contractFormula = Contracts.getContractForumla();

            lbContractFormula.Items.Clear();

            foreach (ContractFormula contractFormula in Global.contractFormula)
            {
                lbContractFormula.Items.Add(String.Format("{0}", contractFormula.Description));
            }
        }
    }
}
