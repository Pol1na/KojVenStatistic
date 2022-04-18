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
    /// Interaction logic for DiseasesPage.xaml
    /// </summary>
    public partial class DiseasesPage : Page
    {
        KojVenStatisticEntities2 db = new KojVenStatisticEntities2();
       
            
       
        public DiseasesPage()
        {
            InitializeComponent();
            DGDiseases.ItemsSource = db.Disease.ToList();
        }
    }
}
