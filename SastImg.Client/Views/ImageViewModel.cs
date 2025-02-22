using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using SastImg.Client.Service.API;

namespace SastImg.Client.Views
{
    public partial class ImageViewModel : ObservableObject
    {
        // 图片数据（使用 ObservableProperty 自动生成属性）
        [ObservableProperty]
        private byte[]? imageData;

        // 获取指定 ID 的图片
        public async Task<bool> ShowImageAsync(long id)
        {
            // 调用 API 获取图片
            var imageResponse = await App.API!.Image.GetImageAsync(id, 0); // 0 表示原图
            if (!imageResponse.IsSuccessful) return false;

            // 将图片数据转换为字节数组
            using var memoryStream = new MemoryStream();
            await imageResponse.Content.CopyToAsync(memoryStream);
            ImageData = memoryStream.ToArray();

            return true;
        }
    }
}
