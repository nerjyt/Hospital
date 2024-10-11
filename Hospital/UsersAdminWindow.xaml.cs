using System;
using System.Collections.Generic;
using System.Data;
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
using Hospital.HospitalDataSetTableAdapters;

namespace Hospital
{
    /// <summary>
    /// Логика взаимодействия для UsersAdminWindow.xaml
    /// </summary>
    public partial class UsersAdminWindow : Window
    {
        UsersTableAdapter users = new UsersTableAdapter();
        AccountTableAdapter account = new AccountTableAdapter();
        public UsersAdminWindow()
        {
            InitializeComponent();

            UsersGrid.ItemsSource = users.GetData();

            UsersAccountComboBox.ItemsSource = account.GetData();
            UsersAccountComboBox.DisplayMemberPath = "id_Account";
            UsersAccountComboBox.SelectedValuePath = "id_Account";

            UsersGenderComboBox.Items.Insert(0, "м");
            UsersGenderComboBox.Items.Insert(1, "ж");
        }

        private void UsersCreate_Click(object sender, RoutedEventArgs e)
        {
            if (UsersSurnameTxb.Text == string.Empty ||
                UsersNameTxb.Text == string.Empty ||
                UsersMidlleNameTxb.Text == string.Empty ||
                UsersGenderComboBox.Text == string.Empty ||
                UsersPhoneTxb.Text == string.Empty ||
                UsersAccountComboBox.Text == string.Empty ||
                (!(UsersSurnameTxb.Text is string)) ||
                (!(UsersNameTxb.Text is string)) ||
                (!(UsersMidlleNameTxb.Text is string)) ||
                (!(UsersPhoneTxb.Text is string)))
            {
                MessageBox.Show("Поле не может быть пустым");
            }
            else
            {
                users.InsertQuery(UsersSurnameTxb.Text, UsersNameTxb.Text, UsersMidlleNameTxb.Text,
                UsersGenderComboBox.Text, UsersPhoneTxb.Text, Convert.ToInt32(UsersAccountComboBox.Text));
                UsersGrid.ItemsSource = users.GetData();
            }
        }

        private void UsersUpdateBt_Click(object sender, RoutedEventArgs e)
        {
            if (UsersSurnameTxb.Text == string.Empty ||
                UsersNameTxb.Text == string.Empty ||
                UsersMidlleNameTxb.Text == string.Empty ||
                UsersGenderComboBox.Text == string.Empty ||
                UsersPhoneTxb.Text == string.Empty ||
                UsersAccountComboBox.Text == string.Empty ||
                (!(UsersSurnameTxb.Text is string)) ||
                (!(UsersNameTxb.Text is string)) ||
                (!(UsersMidlleNameTxb.Text is string)) ||
                (!(UsersPhoneTxb.Text is string)))
            {
                MessageBox.Show("Поле не может быть пустым");
            }
            else if (UsersGrid.SelectedItem != null)
            {
                object id = (UsersGrid.SelectedItem as DataRowView).Row[0];
                users.UpdateQuery(UsersSurnameTxb.Text, UsersNameTxb.Text, UsersMidlleNameTxb.Text, UsersGenderComboBox.Text, UsersPhoneTxb.Text, Convert.ToInt32(UsersAccountComboBox.Text), Convert.ToInt32(id));
                UsersGrid.ItemsSource = users.GetData();
            }
            else
            {
                MessageBox.Show("Не было выбрано поле для изменения");
            }
        }

        private void UsersDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (UsersGrid.SelectedItem as DataRowView).Row[0];
                users.DeleteQuery(Convert.ToInt32(id));
                UsersGrid.ItemsSource = users.GetData();
            }
        }

        private void UsersBackBt_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow().Show();
            Close();
        }

        private void UsersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersGrid.SelectedItem != null)
            {
                object surname = (UsersGrid.SelectedItem as DataRowView).Row[1];
                UsersSurnameTxb.Text = surname as string;
                    
                object name = (UsersGrid.SelectedItem as DataRowView).Row[2];
                UsersNameTxb.Text = name as string;

                object midlleName = (UsersGrid.SelectedItem as DataRowView).Row[3];
                UsersMidlleNameTxb.Text = midlleName as string;

                object gender = (UsersGrid.SelectedItem as DataRowView).Row[4];
                UsersGenderComboBox.SelectedValue = gender;

                object phone = (UsersGrid.SelectedItem as DataRowView).Row[5];
                UsersPhoneTxb.Text = phone as string;

                object id_Account = (UsersGrid.SelectedItem as DataRowView).Row[6];
                UsersAccountComboBox.SelectedValue = id_Account;

            }
            if (UsersGrid.SelectedItem == null)
            {
                UsersGrid.ItemsSource = users.GetData();
            }
        }
    }
}
