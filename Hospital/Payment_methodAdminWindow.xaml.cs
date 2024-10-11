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
    /// Логика взаимодействия для Payment_methodAdminWindow.xaml
    /// </summary>
    public partial class Payment_methodAdminWindow : Window
    {
        Payment_methodTableAdapter payment = new Payment_methodTableAdapter();
        public Payment_methodAdminWindow()
        {
            InitializeComponent();
            PaymentGrid.ItemsSource = payment.GetData();
        }
        private void PaymentCreate_Click(object sender, RoutedEventArgs e)
        {
            if (PaymentTxb.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }
            else
            {
                payment.InsertQuery(PaymentTxb.Text);
                PaymentGrid.ItemsSource = payment.GetData();
            }
        }

        private void PaymentUpdateBt_Click(object sender, RoutedEventArgs e)
        {
            if ((PaymentTxb is string))
            {
                MessageBox.Show("Введены некоректные данные");
            }

            if (PaymentTxb.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }
                
            else if (PaymentGrid.SelectedItem != null)
            {
                object id = (PaymentGrid.SelectedItem as DataRowView).Row[0];
                payment.UpdateQuery(PaymentTxb.Text, Convert.ToInt32(id));
                PaymentGrid.ItemsSource = payment.GetData();
            }
            else
            {
                MessageBox.Show("Не было выбрано поле для изменения");
            }
        }

        private void PaymentDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (PaymentGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (PaymentGrid.SelectedItem as DataRowView).Row[0];
                payment.DeleteQuery(Convert.ToInt32(id));
                PaymentGrid.ItemsSource = payment.GetData();
            }
        }

        private void PaymentBackBt_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow().Show();
            Close();
        }

        private void PaymentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PaymentGrid.SelectedItem != null)
            {
                object cell = (PaymentGrid.SelectedItem as DataRowView).Row[1];
                PaymentTxb.Text = cell as string;
            }
            else if (PaymentGrid.SelectedItem != null)
            {
                PaymentGrid.ItemsSource = payment.GetData();
            }
        }
    }
}
