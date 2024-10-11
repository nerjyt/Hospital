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
    /// Логика взаимодействия для ServiceAdminWindow.xaml
    /// </summary>
    public partial class ServiceAdminWindow : Window
    {
        ServiceeTableAdapter service = new ServiceeTableAdapter();
        DoctorTableAdapter doctor = new DoctorTableAdapter();
        public ServiceAdminWindow()
        {
            InitializeComponent();

            ServiceGrid.ItemsSource = service.GetData();

            ServiceComboBox.ItemsSource = doctor.GetData();
            ServiceComboBox.DisplayMemberPath = "id_Doctor";
            ServiceComboBox.SelectedValuePath = "id_Doctor";
        }

        private void ServiceCreateBt_Click(object sender, RoutedEventArgs e)
        {

            if (ServiceNameTxb.Text == string.Empty || ServiceCostTxb.Text == string.Empty || ServiceComboBox.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }
            else
            {
                service.InsertQuery(ServiceNameTxb.Text, ServiceCostTxb.Text, Convert.ToInt32(ServiceComboBox.Text));
                ServiceGrid.ItemsSource = service.GetData();
            }
        }

        private void ServiceUpdateBt_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceNameTxb.Text == string.Empty || ServiceCostTxb.Text == string.Empty || ServiceComboBox.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }

            else if (ServiceGrid.SelectedItem != null)
            {
                object id = (ServiceGrid.SelectedItem as DataRowView).Row[0];
                service.UpdateQuery(ServiceNameTxb.Text, ServiceCostTxb.Text, Convert.ToInt32(ServiceComboBox.Text), Convert.ToInt32(id));
                ServiceGrid.ItemsSource = service.GetData();
            }
            else
            {
                MessageBox.Show("Не было выбрано поле для изменения");
            }
        }

        private void ServiceDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (ServiceGrid.SelectedItem as DataRowView).Row[0];
                service.DeleteQuery(Convert.ToInt32(id));
                ServiceGrid.ItemsSource = service.GetData();
            }
        }

        private void ServiceBackBt_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow().Show();
            Close();
        }

        private void ServiceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ServiceGrid.SelectedItem != null)
            {
                object name = (ServiceGrid.SelectedItem as DataRowView).Row[1];
                ServiceNameTxb.Text = name as string;
                object cost = (ServiceGrid.SelectedItem as DataRowView).Row[2];
                ServiceCostTxb.Text = cost as string;
                object id_Doctor = (ServiceGrid.SelectedItem as DataRowView).Row[3];
                ServiceComboBox.SelectedValue = id_Doctor;

                if (ServiceGrid.SelectedItem == null)
                {
                    ServiceGrid.ItemsSource = service.GetData();
                }
            }
        }
    }
}
