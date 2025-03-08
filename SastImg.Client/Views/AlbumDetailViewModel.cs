using SastImg.Client.Service.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SastImg.Client.Views
{
    public class AlbumDetailViewModel : INotifyPropertyChanged
    {
       
        public ObservableCollection<ImageDto> Images { get; } = new();
       

       
        public async Task LoadAlbumAsync(long albumId)
        {
            Images.Clear();
            var imagesResponse = await App.API.Image.GetImagesAsync(null,albumId,null);
            foreach (var image in imagesResponse.Content)
            {               
                Images.Add(image);
            }            
        }

        // 实现 INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
   
}
