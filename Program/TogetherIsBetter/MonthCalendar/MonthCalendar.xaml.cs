using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MonthCalendar
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
<<<<<<< HEAD:Program/TogetherIsBetter/MonthCalendar/MonthCalendar.xaml.cs
        public UserControl1()
=======
        private TIB_Model db;
        public MainWindow()
>>>>>>> remotes/origin/master:Program/TogetherIsBetter/TogetherIsBetter/MainWindow.xaml.cs
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (db = new TIB_Model()){
                //get all objects
                var query = from b in db.Company
                            orderby b.Name 
                            select b;
                
                List<Company> companies = new List<Company>();
                foreach (var item in query)
                {
                    companies.Add(item);
                } 

               test1.Text = companies[1].Name;

                //lookup by key
               Location loc = db.Location.Find(1);
               test2.Text = loc.Name;

                //lookup by where
               Location loca = db.Location.Where(b => b.Name.Equals("Room 1")).FirstOrDefault();
               test3.Text = loca.Name;
                
                //all objects in table
               List<Reservation> reserves = new List<Reservation>();
               reserves = db.Reservation.ToList();

               foreach (Reservation r in reserves)
               {
                   testlijst.Items.Add(r.Id + "" + r.Company.Name + r.StartDate.ToString());
               }
            }
        }
    }
}
