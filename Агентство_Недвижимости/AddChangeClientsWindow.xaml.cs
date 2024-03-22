using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

namespace Агентство_Недвижимости
{
    /// <summary>
    /// Логика взаимодействия для AddChangeClientsWindow.xaml
    /// </summary>
    public partial class AddChangeClientsWindow : Window
    {
        Real_Estate_AgencyEntities entity = new Real_Estate_AgencyEntities();
        bool isEdit = false;
        Clients client;
        public AddChangeClientsWindow(bool isEditable, Clients clientInfo)
        {
            InitializeComponent();
            client = clientInfo;
            isEdit = isEditable;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                surnameTB.Text = client.Surname;
                nameTB.Text = client.Name;
                patronymicTB.Text = client.Patronymic;
                phoneTB.Text = client.Phone;
                emailTB.Text = client.Email;
            }
            else
            {
                client = new Clients();
                client.ID = entity.Clients.Max(x => x.ID) + 1;
            }
        }

        private void saveBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((phoneTB.Text.Length >= 0 && phoneTB.Text != "") 
                    || (emailTB.Text.Length >= 0 && emailTB.Text != ""))
                {
                    client.Surname = surnameTB.Text;
                    client.Name = nameTB.Text;
                    client.Patronymic = patronymicTB.Text;
                    client.Phone = phoneTB.Text;
                    client.Email = emailTB.Text;
                    entity.Clients.AddOrUpdate(client);
                    entity.SaveChanges();
                    MessageBox.Show($"Клиент успешно добавлен или отредактирован", "Уведомление");
                    backBttn_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Нельзя создать клиента без электронной почты или телефона",
                        "Уведомление");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Возникла ошибка при добавлений или изменений клиента", "Ошибка");
            }
        }

        private void backBttn_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
