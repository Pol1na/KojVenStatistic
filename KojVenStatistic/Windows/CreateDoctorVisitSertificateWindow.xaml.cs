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
    /// Логика взаимодействия для CreateDoctorVisitSertificateWindow.xaml
    /// </summary>
    public partial class CreateDoctorVisitSertificateWindow : Window
    {
        public CreateDoctorVisitSertificateWindow(Appeal appeal)
        {
            InitializeComponent();
            DataContext = appeal;
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if(dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(VisitSertificate, "Справка о посещении врача");
                DialogResult = true;
                Close();
            }
        }
    }
}
