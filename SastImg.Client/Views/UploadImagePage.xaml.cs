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
            }
        }
       
    }
}
