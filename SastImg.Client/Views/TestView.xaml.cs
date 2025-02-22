using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SastImg.Client.Views;
using SastImg.Client.Service.API;
using Microsoft.UI.Xaml.Navigation;
using Windows.Storage.Pickers;


namespace SastImg.Client.Views
{
    public sealed partial class TestView : Page
    {
        public TestViewModel ViewModel { get; } = new();

        public TestView()
        {
            this.InitializeComponent();
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.GetAllImagesAsync(); // 页面加载时获取图片列表
        }

        private void OnSelectionChanged(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is not ImageDto c) return; // 点击的对象是ImageDto的话（因为ClickItem是object类型的，
                                                         // 为保险需要提前判断一下，并把它转换成ImageDto c）
            App.Shell.MainFrame.Navigate(typeof(ImageView), c.Id); // 除了页面类型，还可以传递参数！
        }
        private async void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            var filePicker = new FileOpenPicker();
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".png");
            var file = await filePicker.PickSingleFileAsync();

            if (file != null)
            {
                using (var stream = await file.OpenReadAsync())
                {
                    var result = await App.API.Image.UploadImageAsync(new ImageUploadRequest
                    {
                        AlbumId = 1, // 替换为实际相册 ID
                        FileStream = stream.AsStream()
                    });

                    if (result.IsSuccessStatusCode)
                    {
                        // 上传成功后刷新图片列表
                        await ViewModel.GetAllImagesAsync();
                    }
                }
            }
        }

        private async void CreateAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            var title = "新相册"; // 替换为实际输入
            var description = "这是一个新相册"; // 替换为实际输入

            var result = await App.API.Album.CreateAlbumAsync(new AlbumCreateRequest
            {
                Title = title,
                Description = description
            });

            if (result.IsSuccessStatusCode)
            {
                // 创建成功后刷新相册列表（如果有）
            }
        }

        private async void EditImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedImage == null)
            {
                return;
            }

            var imageId = ViewModel.SelectedImage.Id;
            var description = "新的描述"; // 替换为实际输入
            var tags = new List<string> { "新标签" }; // 替换为实际输入

            var result = await App.API.Image.UpdateImageAsync(imageId, new ImageUpdateRequest
            {
                Description = description,
                Tags = tags
            });

            if (result.IsSuccessStatusCode)
            {
                // 编辑成功后刷新图片列表
                await ViewModel.GetAllImagesAsync();
            }
        }
    }


}
