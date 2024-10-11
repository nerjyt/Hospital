using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
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
    /// Логика взаимодействия для CabinetAdminWindow.xaml
    /// </summary>
    public partial class CabinetAdminWindow : Window
    {
        CabinetTableAdapter cabinet = new CabinetTableAdapter();
        DepartmentTableAdapter department = new DepartmentTableAdapter();
        public CabinetAdminWindow()
        {
            InitializeComponent();

            CabinetGrid.ItemsSource = cabinet.GetData();

            CabinetComboBox.ItemsSource = department.GetData();
            CabinetComboBox.DisplayMemberPath = "id_Department";
            CabinetComboBox.SelectedValuePath = "id_Department";
        }

        private void CabinetCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CabinetTxb.Text == string.Empty || CabinetComboBox.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }
            else
            {
                cabinet.InsertQuery(CabinetTxb.Text, Convert.ToInt32(CabinetComboBox.Text));
                CabinetGrid.ItemsSource = cabinet.GetData();
            }
        }
        private void CabinetUpdateBt_Click(object sender, RoutedEventArgs e)
        {
            if (CabinetTxb.Text == string.Empty || CabinetComboBox.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }

            else if (CabinetGrid.SelectedItem != null)
            {
                object id = (CabinetGrid.SelectedItem as DataRowView).Row[0];
                cabinet.UpdateQuery(CabinetTxb.Text, Convert.ToInt32(CabinetComboBox.SelectedValue), Convert.ToInt32(id));
                CabinetGrid.ItemsSource = cabinet.GetData();
            }
            else
            {
                MessageBox.Show("Не было выбрано поле для изменения");
            }
        }

        private void CabinetDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (CabinetGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (CabinetGrid.SelectedItem as DataRowView).Row[0];
                cabinet.DeleteQuery(Convert.ToInt32(id));
                CabinetGrid.ItemsSource = cabinet.GetData();
            }
        }

        private void CabinetBackBt_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow().Show();
            Close();
        }

        private void PostGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CabinetGrid.SelectedItem != null)
            {
                object cell = (CabinetGrid.SelectedItem as DataRowView).Row[1];
                CabinetTxb.Text = cell as string;

                object id_Department = (CabinetGrid.SelectedItem as DataRowView).Row[2];
                CabinetComboBox.SelectedValue = id_Department;
            }
            if (CabinetGrid.SelectedItem == null)
            {
                CabinetGrid.ItemsSource = cabinet.GetData();
            }
        }
    }
}
