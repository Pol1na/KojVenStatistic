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
    /// Логика взаимодействия для TicketPreviewWindow.xaml
    /// </summary>
    public partial class TicketPreviewWindow : Window
    {
        public TicketPreviewWindow(Appeal appeal)
        {
            InitializeComponent();
            DataContext = appeal;
        }

        private void BtnTicketPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if(dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(Ticket, "Талон к врачу");
            }
            DialogResult = true;
            Close();
        }
    }
}
