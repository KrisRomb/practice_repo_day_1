using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Агентство_Недвижимости
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Clients> clients;
        public int max;
        public int currentPage = 1, countPage = 20;
        public int page;
        Real_Estate_AgencyEntities entity = new Real_Estate_AgencyEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void agentsBttn_Click(object sender, RoutedEventArgs e)
        {
            var agentWndw = new AgentsWindow();
            agentWndw.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clientsDG.ItemsSource = entity.Clients.ToList();
        }

        private void addBttn_Click(object sender, RoutedEventArgs e)
        {
            var addClientWndw = new AddChangeClientsWindow(false, null);
            addClientWndw.Show();
            this.Close();
        }

        private void deleteBttn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Вы точно хотите удалить?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message.Equals(MessageBoxResult.Yes))
            {
                var client = clientsDG.SelectedItem as Clients;
                if (client != null)
                {
                    if (entity.House_Demands.Any(x => x.ID_Client == client.ID)
                    || entity.Land_Demands.Any(x => x.ID_Client == client.ID)
                    )
                    {
                        MessageBox.Show("Нельзя удалить клиента, связанного с потребностью или предложением", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        entity.Clients.Remove(client);
                        entity.SaveChanges();
                        MessageBox.Show("Данные были успешно удалены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        clientsDG.ItemsSource = entity.Clients.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Не выбрана строка","Уведомление");
                }
            }
        }

        private void FindLineTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Clients> search = clients;
                List <Clients> filtered = new List<Clients>();
                if (FindLineTextBox.Text.Length == 0)
                {
                    filtered = entity.Clients.ToList();
                }
                else
                {
                    string searchText = FindLineTextBox.Text;
                    filtered =
                    search.Where(r => r.Name.ToLower().StartsWith(searchText.ToLower()) |
                    r.Surname.ToString().ToLower().StartsWith(searchText.ToLower()) |
                    r.Patronymic.ToString().ToLower().StartsWith(searchText.ToLower())).ToList();
                }
                clients = filtered.ToList();
                if (clients.Count == 0)
                {
                    MessageBox.Show("По результату поиска ничего не найдено!");
                }
                else
                {
                    Load();
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// Отображение результатов пойска в DataGrid
        /// </summary>
        public void Load()
        {
            List<Clients> List = clients;
            List<Clients> currentPagesList;
            max = clients.Count() / countPage;
            if (page == 0)
            {
                currentPagesList = List.Skip(0).Take(countPage).ToList();
            }
            else
            {
                currentPagesList = List.Skip(page * countPage - countPage).Take(countPage).ToList();
            }
            clientsDG.ItemsSource = currentPagesList;
        }

        private void changeBttn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Вы точно хотите редактировать?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message.Equals(MessageBoxResult.Yes))
            {
                var client = clientsDG.SelectedItem as Clients;
                if (client != null)
                {
                    var edit = new AddChangeClientsWindow(true, client);
                    edit.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не выбрана строка", "Уведомление");
                }
            }
        }
    }
}
