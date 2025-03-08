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
using Refit;


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

   
    }
}
