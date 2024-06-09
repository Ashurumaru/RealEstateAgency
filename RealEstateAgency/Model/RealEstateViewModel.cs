using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RealEstateAgency.Model
{
    public class RealEstateViewModel
    {
        public int Недвижимость_ID { get; set; }
        public string Адрес { get; set; }
        public string Площадь { get; set; }
        public string Количество_комнат { get; set; }
        public decimal? Стоимость { get; set; }
        public string Описание { get; set; }
        public int? Этаж { get; set; }
        public int? Этажность_здания { get; set; }
        public int? Год_постройки { get; set; }
        public bool? Наличие_балкона_лоджии { get; set; }
        public bool? Наличие_парковки { get; set; }
        public string Статус { get; set; }
        public string Владелец { get; set; }
        public string Контактный_номер_телефона { get; set; }
        public int? Количество_фото { get; set; }
        public ImageSource Фото { get; set; } 

    }
}
