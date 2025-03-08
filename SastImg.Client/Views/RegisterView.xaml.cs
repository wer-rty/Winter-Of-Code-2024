using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using Windows.UI.Xaml;


namespace SastImg.Client.Views
{
    public sealed partial class RegisterView : Page
    {
        public RegisterView()
        {
            this.InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            string code = CodeBox.Text;

            // 验证输入
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowError("用户名和密码不能为空");
                return;
            }

            if (password != confirmPassword)
            {
                ShowError("密码和确认密码不一致");
                return;
            }
            if (string.IsNullOrEmpty(code) || code.Length != 6)
            {
                ShowError("请输入有效的六位验证码");
                return;
            }


            // 调用注册 API
            var result = await App.API!.Account.RegisterAsync(new() { Username = username, Password = password, Code= int.Parse(code) });
            if (result.IsSuccessStatusCode)
            {
                // 注册成功后自动登录
                bool loginSuccess = await App.AuthService.LoginAsync(username, password);
                if (loginSuccess)
                {
                    // 登录成功，跳转到主页
                    App.Shell.MainFrame.Navigate(typeof(HomeView));
                }
                else
                {
                    // 登录失败，显示错误信息
                    ShowError("登录失败，请检查用户名和密码是否正确");
                }
            }
            else
            {
                ShowError("注册失败，请稍后重试");
            }
        }

        private void ShowError(string message)
        {
            ErrorTextBlock.Text = message;
            ErrorTextBlock.Visibility = Visibility.Visible;
        }
    }
}