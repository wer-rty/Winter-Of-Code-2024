using SastImg.Client.Service.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SastImg.Client.Views
{
    public class RemoveAlbumViewModel : INotifyPropertyChanged
    {
        // 相册列表
        public ObservableCollection<AlbumDto> Albums { get; } = new();

        // 选中的相册下标
        private int _selectedAlbumIndex = -1; // 默认值为 -1，表示未选择
        public int SelectedAlbumIndex
        {
            get => _selectedAlbumIndex;
            set
            {
                if (_selectedAlbumIndex != value)
                {
                    _selectedAlbumIndex = value;
                    OnPropertyChanged();

                    // 更新 SelectedAlbumId
                    if (_selectedAlbumIndex >= 0 && _selectedAlbumIndex < Albums.Count)
                    {
                        SelectedAlbumId = Albums[_selectedAlbumIndex].Id;
                    }
                    else
                    {
                        SelectedAlbumId = 0; // 未选择或无效下标
                    }
                }
            }
        }

        // 选中的相册 ID
        private long _selectedAlbumId;
        public long SelectedAlbumId
        {
            get => _selectedAlbumId;
            private set
            {
                if (_selectedAlbumId != value)
                {
                    _selectedAlbumId = value;
                    OnPropertyChanged();
                }
            }
        }

        // 加载相册列表
        public async Task LoadAlbumsAsync()
        {

            var albumsResponse = await App.API.Album.GetAlbumsAsync();
            if (albumsResponse.IsSuccessful && albumsResponse.Content != null)
            {
                Albums.Clear(); // 清空现有数据
                foreach (var album in albumsResponse.Content)
                {
                    Albums.Add(album);
                }
            }
        }      

        // 删除相册
        public async Task RemoveAlbumAsync()
        {
            if (SelectedAlbumId == 0)
            {
                Debug.WriteLine("请先选择一个相册");
                return;
            }

            try
            {
                var response = await App.API.Album.RemoveAlbumAsync(SelectedAlbumId);
                if (response.IsSuccessful)
                {
                    Debug.WriteLine("相册删除成功");
                    await LoadAlbumsAsync(); // 重新加载相册列表
                }
                else
                {
                    Debug.WriteLine($"删除相册失败: {response.Error.Message}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"删除相册时发生异常: {ex.Message}");
            }
        }

        // 实现 INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
