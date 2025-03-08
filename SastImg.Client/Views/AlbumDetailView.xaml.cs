using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using SastImg.Client.Service.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastImg.Client.Views
{
    public sealed partial class AlbumDetailView : Page
    {
        public AlbumDetailViewModel ViewModel { get; } = new();

        public AlbumDetailView()
        {
            this.InitializeComponent();
           
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is long albumId)
            {
                await ViewModel.LoadAlbumAsync(albumId);
            }
        }
        // 图片标题点击事件
        private void OnSelectionChanged(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is ImageDto image)
            {
                            
                // 跳转到 ImageAlone 页面，并传递图片信息
                Frame.Navigate(typeof(ImageAlone), image);
            }
        }
        

    }
}
