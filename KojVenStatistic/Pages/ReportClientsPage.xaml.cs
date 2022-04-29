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
    /// Логика взаимодействия для ReportClientsPage.xaml
    /// </summary>
    public partial class ReportClientsPage : Page
    {
        public ReportClientsPage()
        {
            InitializeComponent();
            List<User> users = AppData.Context.User.Where(x => x.PostId == 2).ToList();
            users.Insert(0, new User { LastName = "Все врачи", });
            CBoxDoctors.ItemsSource = users;
            CBoxDoctors.SelectedIndex = 0;
        }

        private void CreateGrafics(List<Appeal> appeals)
        {
            GistoChart.Series.Clear();
            var appealsByDiseases = appeals.GroupBy(x => x.DiseaseText).ToList();

            foreach (var appealsByDisease in appealsByDiseases)
            {
                
                var currSeries = GistoChart.Series.Add(appealsByDisease.Key);
                foreach (var appeal in appealsByDisease)
                {
                    currSeries.Points.AddXY(appeal.DateOfRequest.Date, appealsByDisease.Where(x => appeal.DateOfRequest.Date == x.DateOfRequest.Date).Count());
                }
            }
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dialog = new PrintDialog();
            if(dialog.ShowDialog() == true)
            {
                dialog.PrintVisual(Host, "Статистика");
            }
        }

        private void CBoxDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Appeal> appeals = AppData.Context.Appeal.ToList();

            if(CBoxDoctors.SelectedIndex != 0)
                appeals = appeals.Where(x => x.User == CBoxDoctors.SelectedItem).ToList();

            CreateGrafics(appeals);
        }
    }
}
