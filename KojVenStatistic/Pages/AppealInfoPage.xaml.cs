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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KojVenStatistic.Pages
{
    /// <summary>
    /// Interaction logic for AppealInfoPage.xaml
    /// </summary>
    public partial class AppealInfoPage : Page
    {
        private Appeal _appeal;
        public AppealInfoPage(Appeal appeal)
        {
            InitializeComponent();
            this.DataContext = appeal;
            CBoxType.ItemsSource = AppData.Context.AppealType.ToList();
            CBoxDisease.ItemsSource = AppData.Context.Disease.ToList();
            _appeal = appeal;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            _appeal.Disease = CBoxDisease.SelectedItem as Disease;
            AppData.Context.SaveChanges();
            MessageBox.Show("Диагноз поставлен");
        }

        private void BtnAddMedicine_Click(object sender, RoutedEventArgs e)
        {
            Window editor = new Windows.AddNewMedicineForUserWindow(_appeal);
            editor.ShowDialog();
        }
    }
}
