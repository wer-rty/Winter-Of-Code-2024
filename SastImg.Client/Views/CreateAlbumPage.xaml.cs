using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage;
using SastImg.Client.Service.API;
using WinRT.Interop;
using static System.Net.Mime.MediaTypeNames;
using System.Net.NetworkInformation;
using System.Net.Mime;

namespace SastImg.Client.Views
{
    public sealed partial class CreateAlbumPage : Page
    {
        private long _albumId;
        public CreateAlbumPage()
        {
            this.InitializeComponent();
        }


        // 创建相册按钮点击事件
        private async void CreateAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            string albumName = AlbumNameTextBox.Text;
            string albumDescription = AlbumDescriptionTextBox.Text;

            if (string.IsNullOrWhiteSpace(albumName))
            {
                await ShowMessageAsync("错误", "相册名称不能为空");
                return;
            }

            CreateAlbumRequest album = new()
            {
                Title = albumName,
                Description = albumDescription,
                CategoryId = 1347077431505096704,
                AccessLevel = 4,
            };

            // 调用 API 创建相册
            try
            {
                var response = await App.API!.Album.CreateAlbumAsync(album);
                _albumId = response.Content;
                if (response.IsSuccessful)
                {

                    await ShowMessageAsync("成功", "相册创建成功");
                    Frame.Navigate(typeof(HomeView)); // 返回主页
                }
                else
                {
                    await ShowMessageAsync("错误", response.Error.Message);
                }
            }
            catch (Exception ex)
            {
                await ShowMessageAsync("错误", $"创建相册失败: {ex.Message}");
            }
        }

        // 显示消息对话框
        private async Task ShowMessageAsync(string title, string message)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "确定"
            };

            dialog.XamlRoot = App.MainWindow?.Content.XamlRoot;

            await dialog.ShowAsync();
        }
    }
}
    



