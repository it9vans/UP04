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

        }

        public void AddProductButtonClick(object sender, RoutedEventArgs e)
        {
            //int productId = dbContext.Products.First(p => p.FullName == productTextBox.Text).Id;
            var product = dbContext.Products.First(p => p.FullName == productTextBox.Text);
            InvoiceProduct newInvoiceProduct = new InvoiceProduct
            {
                ProductId = product.Id,
                Product = product,
                ProductCount = Convert.ToInt32(countTextBox.Text)
            };
           
            addableProducts.Add(newInvoiceProduct);
            addableProductsDataGrid.Items.Refresh();
        }

        public void AddInvoiceButtonClick(object sender, RoutedEventArgs e)
        {
            string client = clientTextBox.Text;
        }

    }
}
