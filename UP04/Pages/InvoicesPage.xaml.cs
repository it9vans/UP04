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

            var invoices = dbContext.InvoiceProducts
                .Include(i => i.Product)
                .Include(i => i.Invoice)
                .Include(i => i.Invoice.Client)
                .Include(i => i.Invoice.WarehouseWorker)
                .ToList();

            invoicesDataGrid.ItemsSource = invoices;

        }
    }
}
