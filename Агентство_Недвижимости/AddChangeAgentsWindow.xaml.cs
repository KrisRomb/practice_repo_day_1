using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для AddChangeAgentsWindow.xaml
    /// </summary>
    public partial class AddChangeAgentsWindow : Window
    {
        Real_Estate_AgencyEntities entity = new Real_Estate_AgencyEntities();
        bool isEdit = false;
        Agents agent;
        public AddChangeAgentsWindow(bool toEdit, Agents agentInfo)
        {
            InitializeComponent();
            agent = agentInfo;
            isEdit = toEdit;
        }

        private void backBttn_Click(object sender, RoutedEventArgs e)
        {
            var agentMain = new AgentsWindow();
            agentMain.Show();
            this.Close();
        }

        private void saveBttn_Click(object sender, RoutedEventArgs e)
        {
                if (surnameTB.Text == "")
                {
                    MessageBox.Show("Нельзя создать риелтора без фамилий");
                    return;
                }
                if (nameTB.Text == "")
                {
                    MessageBox.Show("Нельзя создать риелтора без имени");
                    return;
                }
                if (patronymicTB.Text == "")
                {
                    MessageBox.Show("Нельзя создать риэлтора без отчества");
                    return;
                }
                try
                {
                int share = Convert.ToInt32(shareTB.Text);
                }
                catch
                {
                MessageBox.Show("Введена некорректная комиссионная доля");
                return;
                }

            try
            {
                int share = Convert.ToInt32(shareTB.Text);
                if (share > 0 && share <= 100)
                {
                    agent.Surname = surnameTB.Text;
                    agent.Name = nameTB.Text;
                    agent.Patronymic = patronymicTB.Text;
                    agent.Deal_Share = share;
                    entity.Agents.AddOrUpdate(agent);
                    entity.SaveChanges();
                    MessageBox.Show($"Агент успешно добавлен или отредактирован", "Уведомление");
                    backBttn_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Нельзя создать Агента без комиссионной доли",
                        "Уведомление");
                    return;
                }
            }
            catch
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                surnameTB.Text = agent.Surname;
                nameTB.Text = agent.Name;
                patronymicTB.Text = agent.Patronymic;
                shareTB.Text = Convert.ToString(agent.Deal_Share);
            }
            else
            {
                agent = new Agents();
                agent.ID = entity.Agents.Max(x => x.ID) + 1;
            }
        }
    }
}
