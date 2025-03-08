using SastImg.Client.Service.API;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SastImg.Client.Views;

    public class HomeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AlbumDto> Albums { get; } = new();

        // 加载相册列表
        public async Task LoadAlbumsAsync()
        {
            Albums.Clear();

            var albums = await App.API.Album.GetAlbumsAsync();
            foreach (var album in albums.Content)
            {
                Albums.Add(album);
            }
        }

        // 实现 INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
