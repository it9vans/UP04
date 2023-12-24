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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void ProductsButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductsPage());
        }
        private void InvoicesButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new InvoicesPage());
        }
        private void ClientsButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientsPage());
        }
        private void SuppliersButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SuppliersPage());
        }
        private void WarehousesButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WarehousesPage());
        }


    }
}
