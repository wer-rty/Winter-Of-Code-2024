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
        LoadAlbumsAsync().ConfigureAwait(false); // ��ʼ��ʱ��������б�
    }
    private async Task LoadAlbumsAsync()
    {
        await ViewModel.LoadAlbumsAsync();
    }

    // ������¼�
    private void Album_Click(object sender, ItemClickEventArgs e)
    {
        
        if (e.ClickedItem is AlbumDto album)
        {
            // �������������ҳ��
            Frame.Navigate(typeof(AlbumDetailView), album.Id);
        }
    }
    
}
