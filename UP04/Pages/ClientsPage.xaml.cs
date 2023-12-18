using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UP04.Models;

namespace UP04.Pages
{
    /// <summary>
    /// Interaction logic for ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        private ApplicationDbContext dbContext;

        public ClientsPage()
        {
            InitializeComponent(); 
            dbContext = new ApplicationDbContext();

            var clients = dbContext.Clients.ToList();

            clientsDataGrid.ItemsSource = clients;

        }

        private void addClientButtonClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(nameTextBox.Text) || String.IsNullOrEmpty(addressTextBox.Text) || String.IsNullOrEmpty(phoneNumberTextBox.Text))
            {
                MessageBox.Show($"Все поля должны быть заполнены", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dbContext.Clients.Any(c => c.FullName == nameTextBox.Text))
            {
                MessageBox.Show($"Клиент с таким ФИО/наименованием уже создан", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            Client newClient = new Client
            {
                FullName = nameTextBox.Text,
                Address = addressTextBox.Text,
                PhoneNumber = phoneNumberTextBox.Text
            };
            dbContext.Clients.Add(newClient);
            dbContext.SaveChanges();
            NavigationService.Navigate(new ClientsPage());
        }
    }
}
