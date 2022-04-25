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
    /// Interaction logic for AddEditUserWindow.xaml
    /// </summary>
    public partial class AddEditUserWindow : Window
    {
        private User _user;
        private byte[] _img;

        public AddEditUserWindow(User user)
        {
            InitializeComponent();
            CBoxPost.ItemsSource = AppData.Context.Post.ToList();
            CBoxGender.ItemsSource = AppData.Context.Gender.ToList();
            CBoxSpec.ItemsSource = AppData.Context.Category.ToList();
            if (user!=null)
            {
                TBlockHeader.Text = "Редактирование пользователя";
                TBoxEmail.Text = user.Email;
                TBoxExp.Text = user.Experience.ToString();
                TBoxName.Text = user.FirstName;
                TBoxSurname.Text = user.LastName;
                TBoxLogin.Text = user.Login;
                TBoxPassword.Text = user.Password;
                TBoxPhone.Text = user.PhoneNumber;
                CBoxPost.Text = user.Post.Name;
                CBoxGender.Text = user.Gender.Name;
                CBoxSpec.Text = user.Category.Name;
                _img = user.Image;
                ImageUser.DataContext = _img;
            }
            else
            {

                TBlockHeader.Text = "Добавление пользователя";
            }
            _user = user;

        }

        private void BtnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.JPG;*.PNG)|*.JPG;*.PNG";
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    _img = File.ReadAllBytes(ofd.FileName);
                    ImageUser.DataContext = _img;
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_user!=null)
            {
                _user.FirstName = TBoxName.Text;
                _user.LastName = TBoxSurname.Text;
                _user.Login = TBoxLogin.Text;
                _user.Password = TBoxPassword.Text;
                _user.PhoneNumber = TBoxPhone.Text;
                _user.Email = TBoxEmail.Text;
                _user.Post = CBoxPost.SelectedItem as Post;
                _user.Category = CBoxSpec.SelectedItem as Category;
                _user.Gender = CBoxGender.SelectedItem as Gender;
                _user.Experience = Convert.ToInt32(TBoxExp.Text);
                _user.Image = _img;
                AppData.Context.SaveChanges();
            }
            else
            {
                _user = new User()
                {
                    FirstName = TBoxName.Text,
                    LastName = TBoxSurname.Text,
                    Login = TBoxLogin.Text,
                    Email = TBoxEmail.Text,
                    Password = TBoxPassword.Text,
                    PhoneNumber = TBoxPhone.Text,
                    Experience = Convert.ToInt32(TBoxExp.Text),
                    Post = CBoxPost.SelectedItem as Post,
                    Category = CBoxSpec.SelectedItem as Category,
                    Gender = CBoxGender.SelectedItem as Gender,
                    Image = _img,
                };
                AppData.Context.User.Add(_user);
                AppData.Context.SaveChanges();

            }
            DialogResult = true;
            Close();
        }
    }
}
