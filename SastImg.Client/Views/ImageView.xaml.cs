using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using SastImg.Client.Service.API;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Diagnostics;
using Refit;

namespace SastImg.Client.Views
{
    public sealed partial class ImageView : Page
    {
        public ImageViewModel ViewModel { get; } = new();

        public ImageView()
        {
            this.InitializeComponent();
            ViewModel.PropertyChanged += async (sender, e) =>
            {
                if (e.PropertyName == nameof(ViewModel.ImageData))
                {
                    await UpdateImageAsync();
                }
            };
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (App.Shell?.MainFrame.CanGoBack == true) // 判断是否可以返回
            {
                App.Shell.MainFrame.GoBack(); // 返回上一个页面
            }
        }



        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is long imageId)
            {
                // 显示加载提示
                ShowLoadingPanel(true);

                try
                {
                    await ViewModel.ShowImageAsync(imageId); // 加载图片
                }
                catch (Exception ex)
                {
                    // 处理异常（例如记录日志或显示错误消息）
                    Debug.WriteLine($"加载图片失败: {ex.Message}");
                }
                finally
                {
                    // 隐藏加载提示
                    ShowLoadingPanel(false);
                }
            }
        }
        private async Task UpdateImageAsync()
        {
            if (ViewModel.ImageData is null)
            {
                img.Source = null;
                return;
            }

            var stream = new MemoryStream(ViewModel.ImageData);
            var bitmap = new BitmapImage();
            await bitmap.SetSourceAsync(stream.AsRandomAccessStream());
            img.Source = bitmap;
        }
        private void ShowLoadingPanel(bool isVisible)
        {
            // 控制加载提示的显示和隐藏
            loadingPanel.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

       
    }    

}