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

namespace UP04.Pages
{
    /// <summary>
    /// Interaction logic for SelledProductsPage.xaml
    /// </summary>
    public partial class InvoicesPage : Page
    {
        private ApplicationDbContext dbContext;

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



        }
    }
}
