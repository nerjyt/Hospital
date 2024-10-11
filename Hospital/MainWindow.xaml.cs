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
using Hospital.HospitalDataSetTableAdapters;

namespace Hospital
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AccountTableAdapter account = new AccountTableAdapter(); 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Avtorization(object sender, RoutedEventArgs e)
        {
            var allLogins = account.GetData().Rows;
            for(int i = 0; i < allLogins.Count; i++)
            {
                if (allLogins[i][1].ToString() == LoginTbx.Text &&
                    allLogins[i][2].ToString() == PasswordTbx.Password) 
                {
                    int roleId = (int)allLogins[i][3];

                    switch(roleId) 
                    {
                        case 1:
                            new AdminWindow().Show();
                            Close();
                            break;
                        case 3:
                            new UserWindow().Show();
                            Close();
                            break;
                    }
                }   
            }
            if (LoginTbx.Text == string.Empty || PasswordTbx.Password == string.Empty)
            {
                MessageBox.Show("Одно или несколько полей остались пустыми. Повторите попытку");
                LoginTbx.Text = string.Empty;
                PasswordTbx.Password = string.Empty;
            }
            else
            {
                LoginTbx.Text = string.Empty;
                PasswordTbx.Password = string.Empty;
            }
        }
    }
}
