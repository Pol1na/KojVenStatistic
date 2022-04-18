using KojVenStatistic.Entity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        KojVenStatisticEntities2 db = new KojVenStatisticEntities2();

        byte[] _image;
        public Client _currentClient;
        public AddClient()
        {
            InitializeComponent();
            CbGender.ItemsSource = db.Gender.ToList();
        }

        public AddClient(Client client)
        {
            InitializeComponent();
            _currentClient = client;
            CbGender.ItemsSource = db.Gender.ToList();
            UpdateFields();
            ImagePrev.DataContext = _image;

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images |*.png; *.jpg";
            if(ofd.ShowDialog() == true)
            {
                _image = File.ReadAllBytes(ofd.FileName);
                ImagePrev.DataContext = _image;
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            _currentClient = new Client()
            {
                FirstName = TbFirstName.Text,
                LastName = TbLastName.Text,
                NumberPassport = TbNumPass.Text,
                Adress = TbAdress.Text,
                DateOfBirth = DpDateBirth.SelectedDate.Value,
                Snils = TbSnils.Text,
                MedPolis = TbPolis.Text,
                Gender = CbGender.SelectedItem as Gender,
                Photo = _image
            };
            db.Client.Add(_currentClient);
            db.SaveChanges();
            this.Close();
        }

        private void UpdateFields()
        {
            TbFirstName.Text = _currentClient.FirstName;
            TbLastName.Text = _currentClient.LastName;
            TbNumPass.Text = _currentClient.NumberPassport;
            TbAdress.Text = _currentClient.Adress;
            DpDateBirth.SelectedDate = _currentClient.DateOfBirth;
            TbSnils.Text = _currentClient.Snils;
            TbPolis.Text = _currentClient.MedPolis;
            CbGender.SelectedItem = db.Gender.FirstOrDefault(p=>p.Id == _currentClient.GenderId);
            _image = _currentClient.Photo;

        }
    }
}
