using SastImg.Client.Service.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SastImg.Client.Views
{
    public class UploadImageViewModel : INotifyPropertyChanged
    {
        // 相册列表
        public ObservableCollection<AlbumDto> Albums { get; } = new();

        // 选中的相册 ID
        private long _selectedAlbumId;
        public long SelectedAlbumId
        {
            get => _selectedAlbumId;
            set
            {
                _selectedAlbumId = value;
                OnPropertyChanged();
            }
        }

        // 加载相册列表
        public async Task LoadAlbumsAsync()
        {
            var albumsResponse = await App.API.Album.GetAlbumsAsync();
            if (albumsResponse.IsSuccessful)
            {
                foreach (var album in albumsResponse.Content)
                {
                    Albums.Add(album);
                }
            }
        }

        // 实现 INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
