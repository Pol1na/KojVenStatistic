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
    /// Interaction logic for ClientEditorWindow.xaml
    /// </summary>
    public partial class ClientEditorWindow : Window
    {
        public ClientEditorWindow()
        {
            InitializeComponent();
        }
        public ClientEditorWindow(string snils)
        {
            InitializeComponent();
            TBoxSnils.Text = snils.Substring(0, 14);
        }
    }
}
