using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastImg.Client.Views
{
    public sealed partial class RemoveAlbumPage : Page
    {
        public RemoveAlbumViewModel ViewModel => DataContext as RemoveAlbumViewModel;
        public RemoveAlbumPage()
        {
            DataContext = new RemoveAlbumViewModel();
            this.InitializeComponent();
        }
        // 页面加载时加载相册列表
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {           
                await ViewModel.LoadAlbumsAsync();
            AlbumComboBox.ItemsSource = ViewModel.Albums.Select(x => x.Title).ToArray();

        }
        // 删除按钮点击事件
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedAlbumId == 0)
            {              
                return;
            }
            //// 确认删除
            //var confirmDialog = new ContentDialog
            //{
            //    Title = "确认删除",               
            //    PrimaryButtonText = "确定",
            //    CloseButtonText = "取消"
            //};
            //var result = await confirmDialog.ShowAsync();
            //if (result == ContentDialogResult.Primary)
            //{               
            //        await ViewModel.RemoveAlbumAsync();               
            //}
        }
     }
}
