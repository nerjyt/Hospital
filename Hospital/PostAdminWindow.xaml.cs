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
    /// Логика взаимодействия для PostAdminWindow.xaml
    /// </summary>
    public partial class PostAdminWindow : Window
    {
        PostTableAdapter post = new PostTableAdapter();
        public PostAdminWindow()
        {
            InitializeComponent();
            PostGrid.ItemsSource = post.GetData();
        }

        private void PostCreate_Click(object sender, RoutedEventArgs e)
        {
            if (PostTxb.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }
            else
            {
                post.InsertQuery(PostTxb.Text);
                PostGrid.ItemsSource = post.GetData();
            }
        }

        private void PostUpdateBt_Click(object sender, RoutedEventArgs e)
        {
            if ((PostTxb.Text is string))
            {
                MessageBox.Show("Введены некоректные данные");
            }

            if (PostTxb.Text == string.Empty)
            {
                MessageBox.Show("Поле не может быть пустым");
            }

            else if (PostGrid.SelectedItem != null)
            {
                object id = (PostGrid.SelectedItem as DataRowView).Row[0];
                post.UpdateQuery(PostTxb.Text, Convert.ToInt32(id));
                PostGrid.ItemsSource = post.GetData();
            }
            else
            {
                MessageBox.Show("Не было выбрано поле для изменения");
            }
        }

        private void PostDeleteBt_Click(object sender, RoutedEventArgs e)
        {
            if (PostGrid.SelectedItem == null)
            {
                MessageBox.Show("Не было выбрано поле для удаления");
            }
            else
            {
                object id = (PostGrid.SelectedItem as DataRowView).Row[0];
                post.DeleteQuery(Convert.ToInt32(id));
                PostGrid.ItemsSource = post.GetData();
            }
        }

        private void PostBackBt_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow().Show();
            Close();
        }

        private void PostGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PostGrid.SelectedItem != null)
            {
                object cell = (PostGrid.SelectedItem as DataRowView).Row[1];
                PostTxb.Text = cell as string;
            }
            if (PostGrid.SelectedItem == null)
            {
                PostGrid.ItemsSource = post.GetData();
            }
        }
    }
}
