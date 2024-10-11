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
    /// Логика взаимодействия для AccountAdminWindow.xaml
    /// </summary>
    public partial class AccountAdminWindow : Window
    {   
        AccountTableAdapter account = new AccountTableAdapter();
        RolesTableAdapter roles = new RolesTableAdapter();

        public AccountAdminWindow()
        {
            InitializeComponent();
            AccountGrid.ItemsSource = account.GetData();

            AccountComboBox.ItemsSource = roles.GetData();
            AccountComboBox.DisplayMemberPath = "id_Role";
            AccountComboBox.SelectedValuePath = "id_Role";
        }
        private void AccountCreateBt_Click(object sender, RoutedEventArgs e)
        {
            if (AccountLoginTxb.Text == string.Empty || AccountPasswordTxb.Text == string.Empty || AccountComboBox.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }
            else
            {
                account.InsertQuery(AccountLoginTxb.Text, AccountPasswordTxb.Text, Convert.ToInt32(AccountComboBox.Text));
                AccountGrid.ItemsSource = account.GetData();
            }
        }

        private void UpdateAccountBt_Click(object sender, RoutedEventArgs e)
        {
            if (AccountLoginTxb.Text == string.Empty || AccountPasswordTxb.Text == string.Empty || AccountComboBox.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }

            else if (AccountGrid.SelectedItem != null)
            {
                object id = (AccountGrid.SelectedItem as DataRowView).Row[0];
                account.UpdateQuery(AccountLoginTxb.Text, AccountPasswordTxb.Text, Convert.ToInt32(AccountComboBox.SelectedValue), Convert.ToInt32(id));
                AccountGrid.ItemsSource = account.GetData();
            }
            else
            {
                MessageBox.Show("Не было выбрано поле для изменения");
            }
        }

        private void AccountDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (AccountGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (AccountGrid.SelectedItem as DataRowView).Row[0];
                account.DeleteQuery(Convert.ToInt32(id));
                AccountGrid.ItemsSource = account.GetData();
            }
        }

        private void AccountBackBt_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow().Show();
            Close();
        }

        private void AccountGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AccountGrid.SelectedItem != null)
            {
                object login = (AccountGrid.SelectedItem as DataRowView).Row[1];
                AccountLoginTxb.Text = login as string;
                object pass = (AccountGrid.SelectedItem as DataRowView).Row[2];
                AccountPasswordTxb.Text = pass as string;
                object id_Role = (AccountGrid.SelectedItem as DataRowView).Row[3];
                AccountComboBox.SelectedValue = id_Role;
            }
            if (AccountGrid.SelectedItem == null)
            {
                AccountGrid.ItemsSource = account.GetData();
            }
        }
    }
}
