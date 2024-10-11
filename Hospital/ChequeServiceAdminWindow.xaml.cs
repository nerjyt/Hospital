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
    /// Логика взаимодействия для ChequeServiceAdminWindow.xaml
    /// </summary>
    public partial class ChequeServiceAdminWindow : Window
    {
        Cheque_serviceTableAdapter CS = new Cheque_serviceTableAdapter();
        ChequeTableAdapter cheque = new ChequeTableAdapter();
        ServiceeTableAdapter service = new ServiceeTableAdapter();
        public ChequeServiceAdminWindow()
        {
            InitializeComponent();

            ChequeServiceGrid.ItemsSource = CS.GetData();

            ChequeComboBox.ItemsSource = cheque.GetData();
            ChequeComboBox.DisplayMemberPath = "id_Cheque";
            ChequeComboBox.SelectedValuePath = "id_Cheque";

            ServiceComboBox.ItemsSource = service.GetData();
            ServiceComboBox.DisplayMemberPath = "id_Service";
            ServiceComboBox.SelectedValuePath = "id_Service";

        }
        private void ChequeServiceCreateBt_Click(object sender, RoutedEventArgs e)
        {
            if (ChequeComboBox.Text == string.Empty || ServiceComboBox.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }
            else
            {
                CS.InsertQuery(Convert.ToInt32(ChequeComboBox.Text), Convert.ToInt32(ServiceComboBox.Text));
                ChequeServiceGrid.ItemsSource = CS.GetData();
            }
        }
        private void ChequeServiceUpdateBt_Click(object sender, RoutedEventArgs e)
        {
            if (ChequeComboBox.Text == string.Empty || ServiceComboBox.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }
            else if (ChequeServiceGrid.SelectedItem != null)
            {
                object id = (ChequeServiceGrid.SelectedItem as DataRowView).Row[0];
                CS.UpdateQuery(Convert.ToInt32(ChequeComboBox.Text), Convert.ToInt32(ServiceComboBox.Text), Convert.ToInt32(id));
                ChequeServiceGrid.ItemsSource = CS.GetData();
            }
            else
            {
                MessageBox.Show("Не было выбрано поле для изменения");
            }
        }


        private void ChequeDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (ChequeServiceGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (ChequeServiceGrid.SelectedItem as DataRowView).Row[0];
                cheque.DeleteQuery(Convert.ToInt32(id));
                ChequeServiceGrid.ItemsSource = CS.GetData();
            }
        }

        private void ChequeServiceBackBt_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow().Show();
            Close();
        }

        private void ChequeServiceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChequeServiceGrid.SelectedItem != null)
            {
                object id_Cheque = (ChequeServiceGrid.SelectedItem as DataRowView).Row[1];
                ChequeComboBox.SelectedValue = id_Cheque;

                object id_Service = (ChequeServiceGrid.SelectedItem as DataRowView).Row[2];
                ServiceComboBox.SelectedValue = id_Service;

            }
            if (ChequeServiceGrid.SelectedItem == null)
            {
                ChequeServiceGrid.ItemsSource = CS.GetData();
            }
        }

    }
}
