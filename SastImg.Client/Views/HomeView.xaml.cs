using Microsoft.UI.Xaml.Controls;
using SastImg.Client.Service.API;

namespace SastImg.Client.Views;

public sealed partial class HomeView : Page
{
    public HomeViewModel ViewModel { get; }
    public HomeView ( )
    {
        ViewModel = new HomeViewModel();
        this.InitializeComponent();
        LoadAlbumsAsync().ConfigureAwait(false); // 初始化时加载相册列表
    }
    private async Task LoadAlbumsAsync()
    {
        await ViewModel.LoadAlbumsAsync();
    }

    // 相册点击事件
    private void Album_Click(object sender, ItemClickEventArgs e)
    {
        
        if (e.ClickedItem is AlbumDto album)
        {
            // 导航到相册详情页面
            Frame.Navigate(typeof(AlbumDetailView), album.Id);
        }
    }
    
}
