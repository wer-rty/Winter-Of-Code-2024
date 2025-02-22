using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using SastImg.Client.Service.API;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace SastImg.Client.Views
{
    public partial class TestViewModel : ObservableObject
    {
        // 图片列表
        public ObservableCollection<ImageDto> Images { get; } = new();

        // 选中的图片
        [ObservableProperty]
        private ImageDto? selectedImage;

        // 获取所有图片
        [RelayCommand]
        public async Task GetAllImagesAsync()
        {
            Images.Clear();
            var imagesRequest = await App.API!.Image.GetImagesAsync(null, null, null);
            if (!imagesRequest.IsSuccessful) return; // 如果获取失败，直接返回

            foreach (var image in imagesRequest.Content)
            {
                Images.Add(image);
            }
        }
    }


}
