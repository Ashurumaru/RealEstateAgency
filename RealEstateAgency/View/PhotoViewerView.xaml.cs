using RealEstateAgency.Model;
using RealEstateAgency.Utils;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RealEstateAgency.View
{
    public partial class PhotoViewerView : Window
    {
        private List<ФотографииНедвижимости> photos;
        private int currentIndex;

        public PhotoViewerView(List<ФотографииНедвижимости> photos, int initialIndex = 0)
        {
            InitializeComponent();
            this.photos = photos;
            this.currentIndex = initialIndex;
            DisplayImage();
        }

        private void DisplayImage()
        {
            var imageData = photos[currentIndex].Фото;
            DisplayedImage.Source = new ImageManipulation().LoadImageFromDatabase(imageData);
            PhotoIndexLabel.Content = $"Image {currentIndex + 1} of {photos.Count}";
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex < photos.Count - 1)
            {
                currentIndex++;
                DisplayImage();
            }
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                DisplayImage();
            }
        }
    }
}
