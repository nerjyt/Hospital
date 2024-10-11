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

namespace Hospital
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void RolesButton_Click(object sender, RoutedEventArgs e)
        {
            new RolesAdminWindow().Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AccountAdminWindow().Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new Payment_methodAdminWindow().Show(); 
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new PostAdminWindow().Show();
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new DepartmentAdminWindow().Show();
            Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            new CabinetAdminWindow().Show();
            Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            new DoctorAdminWindow().Show();
            Close();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            new UsersAdminWindow().Show();
            Close();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            new ServiceAdminWindow().Show();
            Close();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            new ChequeAdminWindow().Show(); 
            Close();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            new ChequeServiceAdminWindow().Show();
            Close();
        }
    }
}
