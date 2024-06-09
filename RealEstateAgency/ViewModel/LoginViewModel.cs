using RealEstateAgency.Model;
using RealEstateAgency.View;
using RealEstateAgency.View.AdminView;
using RealEstateAgency.View.ClientView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RealEstateAgency.ViewModel
{
    internal class LoginViewModel : BaseViewModel
    {
        public LoginView loginView = LoginView.loginView;
        public static LoginViewModel loginViewModel;
        private UserModel currentUser;
        private string login;
        private string password;
        private string errorMessage;

        public UserModel CurrentUser
        {
            get => currentUser;
            private set
            {
                currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand OpenGitHubCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }
        public LoginViewModel()
        {
            loginViewModel = this;
            LoginCommand = new RelayCommand(SignIn, CanSignIn);
            OpenGitHubCommand = new RelayCommand(OpenGitHub);
        }

        private bool CanSignIn(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        }

        private void SignIn(object parameter)
        {
            using (var context = new kursachEntities())
            {
                var user = context.Пользователи
                    .FirstOrDefault(p => p.Электронная_почта == Login
                    && p.Пароль == Password);

                if (user != null)
                {
                    CurrentUser = new UserModel
                    {
                        Пользователь_ID = user.Пользователь_ID,
                        ФИО = user.ФИО,
                        Роль = user.Роль,
                        Адрес = user.Адрес,
                        Контактный_номер_телефона = user.Контактный_номер_телефона,
                        Электронная_почта = user.Электронная_почта,
                        Дата_регистрации = user.Дата_регистрации,
                        Пароль = user.Пароль
                    };
                    if (user.Роль == "1")
                    {
                        AdminDashboardView dashboard = new AdminDashboardView();
                        dashboard.Show();
                        loginView.Close();
                    }
                    if (user.Роль == "2")
                    {
                        ClientDashboardView dashboard = new ClientDashboardView();
                        dashboard.Show();
                        loginView.Close();
                    }

                }
                else
                {
                    ErrorMessage = "Неправильный логин или пароль";
                }
            }
        }

        private void OpenGitHub(object parameter)
        {
            Process.Start("https://github.com/Ashurumaru");
        }
    }

}
