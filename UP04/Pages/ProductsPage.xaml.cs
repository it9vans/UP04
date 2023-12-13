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
    /// Interaction logic for BoughtProductsPage.xaml
    /// </summary>
    /// 


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
    }
}
