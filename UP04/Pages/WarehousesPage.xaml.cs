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
    /// Interaction logic for WarehousesPage.xaml
    /// </summary>
    public partial class WarehousesPage : Page
    {
        private ApplicationDbContext dbContext;

        public WarehousesPage()
        {
            InitializeComponent();
            dbContext = new ApplicationDbContext();

            wareHousesDataGrid.ItemsSource = dbContext.Warehouses.Include(w => w.WarehouseWorker).ToList();
        }

        private void WarehouseWorkerUpdateButtonClick(object sender, RoutedEventArgs e)
        {
            if(MainWindow.currentUser.Role.RoleName != "Manager")
            {
                MessageBox.Show("У вас недостаточно прав!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (String.IsNullOrEmpty(warehouseTextBox.Text) || String.IsNullOrEmpty(warehouseWorkerIdTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int wareHouseId = 0;
            int warehouseWorkerId = 0;
            try
            {
                wareHouseId = Convert.ToInt32(warehouseTextBox.Text);
                warehouseWorkerId = Convert.ToInt32(warehouseWorkerIdTextBox.Text);
                if(wareHouseId == 0 || warehouseWorkerId == 0)
                {
                    MessageBox.Show("Неправильный формат заполнения! Введите Id склада и ответственного!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch 
            {
                MessageBox.Show("Неправильный формат заполнения! Введите Id склада и ответственного!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            Warehouse editableWarehouse = dbContext.Warehouses.Find(wareHouseId);

            if (editableWarehouse == null)
            {
                MessageBox.Show("Склад с таким Id не найден!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            editableWarehouse.UserId = Convert.ToInt32(warehouseWorkerId);
            dbContext.SaveChanges();
            MessageBox.Show("Успех", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new WarehousesPage());
        }

    }
}
