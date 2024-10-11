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
    /// Логика взаимодействия для DepartmentAdminWindow.xaml
    /// </summary>
    public partial class DepartmentAdminWindow : Window
    {
        DepartmentTableAdapter department = new DepartmentTableAdapter();
        public DepartmentAdminWindow()
        {
            InitializeComponent();
            DepartmentGrid.ItemsSource = department.GetData();
        }

        private void DepartmentCreate_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentTxb.Text == string.Empty || DepartmentFloorTxb.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }
            else
            {
                department.InsertQuery(DepartmentTxb.Text, DepartmentFloorTxb.Text) ;
                DepartmentGrid.ItemsSource = department.GetData();
            }
        }

        private void DepartmentUpdateBt_Click(object sender, RoutedEventArgs e)
        {
            if ((!(DepartmentTxb.Text is string)))
            {
                MessageBox.Show("Введены некоректные данные");
            }

            if (DepartmentTxb.Text == string.Empty || DepartmentFloorTxb.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }

            else if (DepartmentGrid.SelectedItem != null && (DepartmentTxb.Text is string))
            {
                object id = (DepartmentGrid.SelectedItem as DataRowView).Row[0];
                department.UpdateQuery(DepartmentTxb.Text, DepartmentFloorTxb.Text, Convert.ToInt32(id));
                DepartmentGrid.ItemsSource = department.GetData();
            }
            else
            {
                MessageBox.Show("Не было выбрано поле для изменения");
            }
        }

        private void DepartmentDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (DepartmentGrid.SelectedItem as DataRowView).Row[0];
                department.DeleteQuery(Convert.ToInt32(id));
                DepartmentGrid.ItemsSource = department.GetData();
            }
        }

        private void DepartmentBackBt_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow().Show();
            Close();
        }

        private void DepartmentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartmentGrid.SelectedItem != null)
            {
                object cell = (DepartmentGrid.SelectedItem as DataRowView).Row[1];
                DepartmentTxb.Text = cell as string;
                object floor = (DepartmentGrid.SelectedItem as DataRowView).Row[2];
                DepartmentFloorTxb.Text = floor as string;
            } 
        }
    }
}
