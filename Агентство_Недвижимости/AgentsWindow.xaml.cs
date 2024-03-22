using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AgentsWindow.xaml
    /// </summary>
    public partial class AgentsWindow : Window
    {
        public List<Agents> agents;
        public int max;
        public int currentPage = 1, countPage = 20;
        public int page;
        Real_Estate_AgencyEntities entity = new Real_Estate_AgencyEntities();
        public AgentsWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            agentsDG.ItemsSource=entity.Agents.ToList();
        }

        private void clientBttn_Click(object sender, RoutedEventArgs e)
        {
            var clientWndw = new MainWindow();
            clientWndw.Show();
            this.Close();
        }

        private void FindLineTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Agents> search = agents;
                List<Agents> filtered = new List<Agents>();
                if (FindLineTextBox.Text.Length == 0)
                {
                    filtered = entity.Agents.ToList();
                }
                else
                {
                    string searchText = FindLineTextBox.Text;
                    filtered =
                    search.Where(r => r.Name.ToLower().StartsWith(searchText.ToLower()) |
                    r.Surname.ToString().ToLower().StartsWith(searchText.ToLower()) |
                    r.Patronymic.ToString().ToLower().StartsWith(searchText.ToLower())).ToList();
                }
                agents = filtered.ToList();
                if (agents.Count == 0)
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
        public void Load()
        {
            List<Agents> List = agents;
            List<Agents> currentPagesList;
            max = agents.Count() / countPage;
            if (page == 0)
            {
                currentPagesList = List.Skip(0).Take(countPage).ToList();
            }
            else
            {
                currentPagesList = List.Skip(page * countPage - countPage).Take(countPage).ToList();
            }
            agentsDG.ItemsSource = currentPagesList;
        }
        private void addBttn_Click(object sender, RoutedEventArgs e)
        {
            var add = new AddChangeAgentsWindow(false, null);
            add.Show();
            this.Close();
        }

        private void changeBttn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Вы точно хотите редактировать?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message.Equals(MessageBoxResult.Yes))
            {
                var agent = agentsDG.SelectedItem as Agents;
                if (agent != null)
                {
                    var edit = new AddChangeAgentsWindow(true, agent);
                    edit.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не выбрана строка", "Уведомление");
                }
            }
        }

        private void deleteBttn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Вы точно хотите удалить?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message.Equals(MessageBoxResult.Yes))
            {
                var agent = agentsDG.SelectedItem as Agents;
                if (agent != null)
                {
                    if (entity.House_Demands.Any(x => x.ID_Agent == agent.ID)
                    || entity.Land_Demands.Any(x => x.ID_Agent == agent.ID)
                    )
                    {
                        MessageBox.Show("Нельзя удалить риэлотора, связанного с потребностью или предложением", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        entity.Agents.Remove(agent);
                        entity.SaveChanges();
                        MessageBox.Show("Данные были успешно удалены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        agentsDG.ItemsSource = entity.Clients.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Не выбрана строка", "Уведомление");
                }
            }
        }
    }
}
