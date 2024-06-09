using Microsoft.Win32;
using RealEstateAgency.Model;
using RealEstateAgency.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RealEstateAgency.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для RealEstateAgencyAdminPanel.xaml
    /// </summary>
    public partial class RealEstateAgencyAdminPanel : Page
    {
        private ФотографииНедвижимости selectedPhoto;
        private List<ФотографииНедвижимости> currentPropertyPhotos;
        private ImageManipulation imageManipulation = new ImageManipulation();

        public RealEstateAgencyAdminPanel()
        {
            InitializeComponent();
            LoadRealEstateData();
            DataGridRealEstate.SelectionChanged += DataGridRealEstate_SelectionChanged;

        }
        private void LoadRealEstateData()
        {
            using (var context = new kursachEntities())
            {
                DataGridRealEstate.ItemsSource = context.Недвижимость.ToList();
            }
        }

        private void DataGridRealEstate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridRealEstate.SelectedItem is Недвижимость selectedProperty)
            {
                LoadPhotos(selectedProperty.Недвижимость_ID);
                LoadOwnerData(selectedProperty);
            }
        }
        private void LoadOwnerData(Недвижимость property)
        {
            using (var context = new kursachEntities())
            {
                DataGridOwner.ItemsSource = context.Пользователи.Where(p => p.Пользователь_ID == property.Владелец_ID).ToList();
                DataGridRealEstateValuations.ItemsSource = context.ОценкиНедвижимости.Where(p => p.Недвижимость_ID == property.Недвижимость_ID).ToList();
                DataGridRealEstateValuations.ItemsSource = context.ОперацииСНедвижимостью.Where(p => p.Договоры.Недвижимость_ID == property.Недвижимость_ID).ToList();

            }
        }
        private void LoadPhotos(int propertyId)
        {
            using (var context = new kursachEntities())
            {
                WrapPanelPhotos.Children.Clear();
                currentPropertyPhotos = context.ФотографииНедвижимости.Where(p => p.Недвижимость_ID == propertyId).ToList();
                foreach (var photo in currentPropertyPhotos)
                {
                    var imageSource = imageManipulation.LoadImageFromDatabase(photo.Фото);
                    var image = new Image
                    {
                        Source = imageSource,
                        Width = 400,
                        Height = 400,
                        
                        Margin = new Thickness(5),
                        ToolTip = "Click to select",
                        RenderTransformOrigin = new Point(0.5, 0.5),
                        RenderTransform = new ScaleTransform(1, 1)
                    };

                    image.MouseEnter += (s, e) => image.Opacity = 0.8;
                    image.MouseLeave += (s, e) => image.Opacity = 1;
                    image.MouseDown += (s, e) =>
                    {
                        if (e.ClickCount == 1) 
                        {
                            selectedPhoto = photo;
                        }
                        if (e.ClickCount == 2)
                        {
                            int index = currentPropertyPhotos.IndexOf(photo);
                            PhotoViewerView viewer = new PhotoViewerView(currentPropertyPhotos, index);
                            viewer.Show();
                        }
                    };



                    WrapPanelPhotos.Children.Add(image);
                }
            }
        }



        private void AddPhotoToRealEstate_Click(object sender, RoutedEventArgs e)
        {
            var selectedRealEstate = DataGridRealEstate.SelectedItem as Недвижимость;
            if (selectedRealEstate == null)
            {
                MessageBox.Show("Выберите недвижимость для добавления фотографий.");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    try
                    {
                        BitmapImage bitmapImage = new BitmapImage(new Uri(filename));
                        byte[] imageData = imageManipulation.ConvertImageToByteArray(bitmapImage);
                        using (var context = new kursachEntities())
                        {
                            var newPhoto = new ФотографииНедвижимости
                            {
                                Недвижимость_ID = ((Недвижимость)DataGridRealEstate.SelectedItem).Недвижимость_ID,
                                Фото = imageData
                            };
                            context.ФотографииНедвижимости.Add(newPhoto);
                            context.SaveChanges();
                            LoadPhotos(selectedRealEstate.Недвижимость_ID);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при добавлении фотографии: {ex.Message}");
                    }
                }
            }
        }
        private void DeletePhoto_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPhoto != null)
            {
                using (var context = new kursachEntities())
                {
                    var photoToDelete = context.ФотографииНедвижимости.Find(selectedPhoto.Фото_ID);
                    if (photoToDelete != null)
                    {
                        context.ФотографииНедвижимости.Remove(photoToDelete);
                        context.SaveChanges();
                        LoadPhotos((int)selectedPhoto.Недвижимость_ID);
                        selectedPhoto = null;
                    }
                    else
                    {
                        MessageBox.Show("Выбранная фотография не найдена в базе данных.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите фотографию для удаления.");
            }
        }
    }
}
