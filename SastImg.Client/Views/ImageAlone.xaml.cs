using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using SastImg.Client.Service.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SastImg.Client.Views
{
    public sealed partial class ImageAlone : Page
    {
        public ObservableCollection<ImageDto> Images { get; } = new();
        public ImageAlone()
        {
            this.InitializeComponent();
        }
       
        public async Task GetImagesAsync()
        {

            Images.Clear();
            var imagesRequest = await App.API!.Image.GetImagesAsync(null, null, null);
            if (!imagesRequest.IsSuccessful)
            {
                // 如果获取失败，返回
                return;
            }

            foreach (var image in imagesRequest.Content)
            {
                Images.Add(image);

            }
        }
        private async Task LoadImageAsync(byte[] imageData)
        {
            // 将 byte[] 转换为 BitmapImage
            using (var stream = new MemoryStream(imageData))
            {
                var bitmap = new BitmapImage();
                await bitmap.SetSourceAsync(stream.AsRandomAccessStream());
                ImageControl.Source = bitmap;
            }
        }


    }
}
