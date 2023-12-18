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
    /// Interaction logic for SelledProductsPage.xaml
    /// </summary>
    public partial class InvoicesPage : Page
    {
        private ApplicationDbContext dbContext;
        private List<InvoiceProduct> addableProducts = new List<InvoiceProduct>();
        Invoice creatableInvoice = new Invoice();


        public InvoicesPage()
        {
            InitializeComponent();

            dbContext = new ApplicationDbContext();

            var invoices = dbContext.Invoices
                .Include(i => i.WarehouseWorker)
                .Include(i => i.Client)
                .Include(i => i.InvoiceProducts)
                .ThenInclude(i => i.Product)
                .ToList();

            getInvoicesList.ItemsSource = invoices.Where(i => i.InvoiceType == "Get");
            sellInvoicesList.ItemsSource = invoices.Where(i => i.InvoiceType == "Sell");

            addableProductsDataGrid.ItemsSource = addableProducts;
            invoiceTypeCombobox.ItemsSource = new string[]
            {
                "Get",
                "Sell"
            };


        }

        public void AddProductButtonClick(object sender, RoutedEventArgs e)
        {
            //int productId = dbContext.Products.First(p => p.FullName == productTextBox.Text).Id;
            var product = dbContext.Products.First(p => p.FullName == productTextBox.Text);
            if (product is null)
            {
                MessageBox.Show($"Товар {productTextBox.Text} не найден. Проверьте правильность заполнения", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (String.IsNullOrEmpty(countTextBox.Text) || Convert.ToInt32(countTextBox.Text) <= 0)
            {
                MessageBox.Show($"Количество товара должно быть > 0", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            InvoiceProduct newInvoiceProduct = new InvoiceProduct
            {
                ProductId = product.Id,
                Product = product,
                ProductCount = Convert.ToInt32(countTextBox.Text)
            };
            creatableInvoice.TotalPrice += product.Price * newInvoiceProduct.ProductCount;
            addableProducts.Add(newInvoiceProduct);
            addableProductsDataGrid.Items.Refresh();
        }

        public void AddInvoiceButtonClick(object sender, RoutedEventArgs e)
        {
            if (!addableProducts.Any())
            {
                MessageBox.Show("Список товаров не должен быть пустым", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string client = clientTextBox.Text;
            
            if (String.IsNullOrEmpty(client)){
                MessageBox.Show("Поле клиента не должно быть пустым", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string invoiceType = invoiceTypeCombobox.Text;
            if (String.IsNullOrEmpty(invoiceType))
            {
                MessageBox.Show("Поле типа накладной не должно быть пустым", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            creatableInvoice.ClientId = dbContext.Clients.First(c => c.FullName == client).Id;
            creatableInvoice.InvoiceType = invoiceType;
            creatableInvoice.WarehouseWorkerId = MainWindow.currentUser.Id;

            dbContext.Invoices.Add(creatableInvoice);
            dbContext.SaveChanges();

            foreach (var invoiceProduct in addableProducts)
            {
                Product addableProductType = dbContext.Products.Find(invoiceProduct.ProductId);
                if (invoiceType == "Sell")
                {
                    if (addableProductType.Count < invoiceProduct.ProductCount)
                    {
                        MessageBox.Show($"Невозможно продать товар {invoiceProduct.Product.FullName}. Введённое кол-во превышает доступное на складе.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        dbContext.Invoices.Remove(creatableInvoice);
                        dbContext.SaveChanges();
                        creatableInvoice = null;
                        return;
                    }
                    else
                    {
                        invoiceProduct.InvoiceId = creatableInvoice.Id;
                        dbContext.InvoiceProducts.Add(invoiceProduct);
                        dbContext.SaveChanges();
                        MessageBox.Show($"Добавил", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else if (invoiceType == "Get")
                {
                    invoiceProduct.InvoiceId = creatableInvoice.Id;
                    dbContext.InvoiceProducts.Add(invoiceProduct);
                    dbContext.SaveChanges();
                    MessageBox.Show($"Добавил", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            sellInvoicesList.Items.Refresh();
            getInvoicesList.Items.Refresh();

            long creatableInvoiceTempPrice = creatableInvoice.TotalPrice;

            //creatableInvoice = null;
            //creatableInvoice = new Invoice()
            //{
            //    //TotalPrice = creatableInvoiceTempPrice
            //};
            //addableProducts.Clear();
            //addableProductsDataGrid.Items.Refresh();
            NavigationService.Navigate(new InvoicesPage());
        }

    }
}
