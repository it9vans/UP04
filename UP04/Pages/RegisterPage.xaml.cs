using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    public partial class RegisterPage : Page
    {
        private ApplicationDbContext dbContext;

        public RegisterPage()
        {
            InitializeComponent();
            dbContext = new ApplicationDbContext();
        }

        private void registerButtonClick(object sender, RoutedEventArgs e)
        {
            string enteredLogin = loginTextBox.Text;
            string enteredPassword = passwordBox.Password.ToString();
            string enteredCheckPassword = checkPasswordBox.Password.ToString();
            string enteredFullName = fullNameTextBox.Text;

            if(IsRegistrationValid(enteredLogin, enteredPassword, enteredCheckPassword, enteredFullName))
            {
                User newUser = new User();
                newUser.Login = enteredLogin;
                newUser.Password = enteredPassword;
                newUser.FullName = enteredFullName;

                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();

                MainWindow.currentUser = dbContext.Users
                    .FirstOrDefault(u => u.Login == enteredLogin);
                NavigationService.Navigate(new MainMenuPage());
            }
            
        }

        public bool IsRegistrationValid(string enteredLogin, string enteredPassword, string enteredCheckPassword, string enteredFullName)
        {
            if (String.IsNullOrWhiteSpace(enteredLogin) || String.IsNullOrWhiteSpace(enteredPassword) || String.IsNullOrWhiteSpace(enteredCheckPassword) || String.IsNullOrWhiteSpace(enteredFullName))
            {
                MessageBox.Show("Данне не должны быть пусты", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (enteredPassword != enteredCheckPassword)
            {
                MessageBox.Show("Введённые пароли не совпадают", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (dbContext.Users.Any(p => p.Login == enteredLogin))
            {
                MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            return true;

        }
    }
}
