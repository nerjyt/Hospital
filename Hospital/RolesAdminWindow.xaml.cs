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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Hospital.HospitalDataSetTableAdapters;

namespace Hospital
{
    /// <summary>
    /// Логика взаимодействия для RolesAdminWindow.xaml
    /// </summary>
    public partial class RolesAdminWindow : Window
    {
        RolesTableAdapter roles = new RolesTableAdapter();
        public RolesAdminWindow()
        {
            InitializeComponent();
            RolesGrid.ItemsSource = roles.GetData();
        }
        private void RoleCreate_Click(object sender, RoutedEventArgs e)
        {
            if (RoleTxb.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }
            else
            {
                roles.InsertQuery(RoleTxb.Text);
                RolesGrid.ItemsSource = roles.GetData();
            }
        }
        private void UpdateRoleBt_Click(object sender, RoutedEventArgs e)
        {
            if (!(RoleTxb.Text is string))
            {
                MessageBox.Show("Введены некоректные данные");
            }

            if (RoleTxb.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }

            else if (RolesGrid.SelectedItem != null)
            {
                object id = (RolesGrid.SelectedItem as DataRowView).Row[0];
                roles.UpdateQuery(RoleTxb.Text, Convert.ToInt32(id));
                RolesGrid.ItemsSource = roles.GetData();
            }
            else
            {
                MessageBox.Show("Не было выбрано поле для изменения");
            }
        }

        private void RoleDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (RolesGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (RolesGrid.SelectedItem as DataRowView).Row[0];
                roles.DeleteQuery(Convert.ToInt32(id));
                RolesGrid.ItemsSource = roles.GetData();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow().Show();
            Close();
        }

        private void RolesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RolesGrid.SelectedItem != null)
            {
                object cell = (RolesGrid.SelectedItem as DataRowView).Row[1];
                RoleTxb.Text = cell as string;
            }
            if (RolesGrid.SelectedItem == null)
            {
               RolesGrid.ItemsSource = roles.GetData();
            }
        }
    }
}
