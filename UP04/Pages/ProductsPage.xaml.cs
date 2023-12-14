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
    public partial class ProductsPage : Page
    {
        private ApplicationDbContext dbContext;

        public ProductsPage()
        {
            InitializeComponent();

            dbContext = new ApplicationDbContext();

            var products = dbContext.Products
                .Include(p => p.Supplier)
                .ToList();

            productsDataGrid.ItemsSource = products;
        }

        private void PostProductButtonClick(object sender, RoutedEventArgs e)
        {
            if (IsPostProductValid(fullNameTextBox.Text, shortNameTextBox.Text, manufacturerTextBox.Text, Convert.ToInt32(supplierIdTextBox.Text), Convert.ToInt32(priceTextBox.Text)))
            {
                Product newProduct = new Product
                {
                    FullName = fullNameTextBox.Text,
                    ShortName = shortNameTextBox.Text,
                    Manufacturer = manufacturerTextBox.Text,
                    SupplierId = Convert.ToInt32(supplierIdTextBox.Text),
                    Price = Convert.ToInt32(priceTextBox.Text)
                };
                dbContext.Products.Add(newProduct);
                dbContext.SaveChanges();
                MessageBox.Show("Товар добавлен в базу данных", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ProductsPage());
            }
            else
            {
                MessageBox.Show("Введены неверные данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsPostProductValid(string fullName, string shortName, string manufacturer, int supplierId, int count)
        {
            if (String.IsNullOrWhiteSpace(fullName) || String.IsNullOrWhiteSpace(shortName) || String.IsNullOrWhiteSpace(manufacturer))
            {
                return false;
            }
            if(supplierId <= 0 || count <= 0)
            {
                return false;
            }
            if (dbContext.Suppliers.Any(p => p.FullName == fullName))
            {
                return false;
            }
            if (!dbContext.Suppliers.Any(p => p.Id == supplierId))
            {
                return false;
            }
            return true;
        }
    }
}
