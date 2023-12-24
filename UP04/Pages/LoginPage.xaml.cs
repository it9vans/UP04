using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace UP04.Pages
{
    public partial class LoginPage : Page
    {
        private ApplicationDbContext dbContext;

        public LoginPage()
        {
            InitializeComponent();
            dbContext = new ApplicationDbContext();
        }
        
        private void RegistrationButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }

            private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            string enteredUsername = loginTextBox.Text;
            string enteredPassword = passwordBox.Password.ToString();


            if (IsLoginValid(enteredUsername, enteredPassword))
            {
                MainWindow.currentUser = dbContext.Users.Include(u => u.Role)
                    .FirstOrDefault(u => u.Login == enteredUsername);
                NavigationService.Navigate(new MainMenuPage()); 
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль. Попробуйте снова.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsLoginValid(string username, string password)
        {
            if(String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                return false;
            }
            var user = dbContext.Users.FirstOrDefault(u => u.Login == username);

            if (user.Password == password)
                return true;
            else
                return false;
        }
    }
}
