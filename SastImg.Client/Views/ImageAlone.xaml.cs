using Microsoft.UI.Xaml;
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
        public ImageAlone()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is ImageDto image)
            {
                         
                    // 加载图片数据
                    var imageResponse = await App.API.Image.GetImageAsync(image.Id, 0); 
                    if (imageResponse.IsSuccessful)
                    {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageResponse.Content.CopyToAsync(memoryStream);
                        byte[] imageData = memoryStream.ToArray();

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
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (App.Shell?.MainFrame.CanGoBack == true) // 判断是否可以返回
            {
                App.Shell.MainFrame.GoBack(); // 返回上一个页面
            }
        }

    }
    

}

