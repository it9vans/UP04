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
    /// Interaction logic for SuppliersPage.xaml
    /// </summary>
    public partial class SuppliersPage : Page
    {
        private ApplicationDbContext dbContext;

        public SuppliersPage()
        {
            InitializeComponent();
            dbContext = new ApplicationDbContext();

            var suppliers = dbContext.Suppliers.ToList();

            suppliersDataGrid.ItemsSource = suppliers;

        }

        private void addSupplierButtonClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(nameTextBox.Text) || String.IsNullOrEmpty(addressTextBox.Text) || String.IsNullOrEmpty(phoneNumberTextBox.Text))
            {
                MessageBox.Show($"Все поля должны быть заполнены", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dbContext.Clients.Any(c => c.FullName == nameTextBox.Text))
            {
                MessageBox.Show($"Поставщик с таким наименованием уже создан", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
            Supplier newSupplier = new Supplier
            {
                FullName = nameTextBox.Text,
                Address = addressTextBox.Text,
                PhoneNumber = phoneNumberTextBox.Text
            };
            dbContext.Suppliers.Add(newSupplier);
            dbContext.SaveChanges();
            NavigationService.Navigate(new SuppliersPage());
        }

    }
}
