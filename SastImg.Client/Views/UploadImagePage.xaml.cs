using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage;
using WinRT.Interop;

namespace SastImg.Client.Views
{
    public sealed partial class UploadImagePage : Page
    {
        public UploadImageViewModel ViewModel => DataContext as UploadImageViewModel;

        public UploadImagePage()
        {
            DataContext = new UploadImageViewModel();
            this.InitializeComponent();

        }

        // 页面加载时加载相册列表
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadAlbumsAsync();
            AlbumComboBox.ItemsSource = ViewModel.Albums.Select(x => x.Title).ToArray();
        }

        // 上传按钮点击事件
        private async void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedAlbumId == 0)
            {
                await ShowErrorMessageAsync("请先选择一个相册");
                return;
            }

            // 选择图片文件
            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
            };
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            nint windowHandle = WindowNative.GetWindowHandle(App.MainWindow);
            InitializeWithWindow.Initialize(openPicker, windowHandle);

            // 选择文件
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                try
                {
                    // 读取文件流
                    using var stream = await file.OpenStreamForReadAsync();

                    // 创建 StreamPart
                    var streamPart = new StreamPart(stream, file.Name, contentType: "image/*");

                    // 调用 API 上传图片
                    var response = await App.API.Image.AddImageAsync(
                        albumId: ViewModel.SelectedAlbumId,
                        title: file.Name,
                        image: streamPart,
                        tags: null
                    );

                    // 检查 API 响应
                    if (response.IsSuccessful)
                    {
                        await ShowMessageAsync("上传成功", "图片已成功上传到相册。");
                    }
                    else
                    {
                        await ShowErrorMessageAsync($"上传失败: {response.Error.Message}");
                    }
                }
                catch (Exception ex)
                {
                    await ShowErrorMessageAsync($"上传图片失败: {ex.Message}");
                }
            }
        }

        // 显示错误消息
        private async Task ShowErrorMessageAsync(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "错误",
                Content = message,
                CloseButtonText = "确定"
            };
            await dialog.ShowAsync();
        }

        // 显示成功消息
        private async Task ShowMessageAsync(string title, string message)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "确定"
            };
            await dialog.ShowAsync();
        }
    }
}
