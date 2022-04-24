using KojVenStatistic.Entity;
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

namespace KojVenStatistic.Windows
{
    /// <summary>
    /// Interaction logic for AddMedicamentWindow.xaml
    /// </summary>
    public partial class AddMedicamentWindow : Window
    {
        public AddMedicamentWindow()
        {
            InitializeComponent();
            CBoxDisease.ItemsSource = AppData.Context.Disease.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            var medicament = new Medicament()
            {
                Name = TBoxName.Text,
                Description = TBoxDescription.Text,
//                Disease = CBoxDisease.SelectedItem as Disease,
            };
            AppData.Context.Medicament.Add(medicament);
            AppData.Context.SaveChanges();
           
        }
    }
}
