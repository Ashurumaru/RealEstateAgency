using RealEstateAgency.Model;
using RealEstateAgency.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RealEstateAgency.View.Pages
{
    public partial class RealEstateAgencyList : Page
    {
        private RealEstateViewModel SelectedItem;
        private List<RealEstateViewModel> realEstateViewModels = new List<RealEstateViewModel>();
        public RealEstateAgencyList()
        {
            InitializeComponent();
            var context = new kursachEntities();
            ImageManipulation imageManipulation = new ImageManipulation();
            var realEstatesFromDb = context.View_НедвижимостьДетали.ToList();

            foreach (var v in realEstatesFromDb)
            {
                RealEstateViewModel viewModel = new RealEstateViewModel
                {
                    Недвижимость_ID = v.Недвижимость_ID,
                    Адрес = v.Адрес,
                    Площадь = $"{v.Площадь} м²",
                    Количество_комнат = $"{v.Количество_комнат}-комн. {v.Тип_недвижимости}",
                    Стоимость = v.Стоимость,
                    Описание = v.Описание,
                    Этаж = v.Этаж,
                    Этажность_здания = v.Этажность_здания,
                    Год_постройки = v.Год_постройки,
                    Наличие_балкона_лоджии = v.Наличие_балкона_лоджии,
                    Наличие_парковки = v.Наличие_парковки,
                    Статус = v.Статус,
                    Владелец = v.Владелец,
                    Контактный_номер_телефона = v.Контактный_номер_телефона,
                    Количество_фото = v.Количество_фото,
                    Фото = imageManipulation.LoadImageFromDatabase(v.Фотография)
                };

                realEstateViewModels.Add(viewModel);
            }

            ItemsList.ItemsSource = realEstateViewModels;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                var select = button.CommandParameter as RealEstateViewModel;
                if (select != null)
                {
                    SelectedItem = select;
                    MessageBox.Show($"Недвижимость(для проверки пкрехода): {select.Недвижимость_ID} ");
                }
            }
        }
    }
}
