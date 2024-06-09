using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Model
{
    internal class UserModel
    {
        public int Пользователь_ID { get; set; }
        public string ФИО { get; set; }
        public string Роль { get; set; }
        public string Адрес { get; set; }
        public string Контактный_номер_телефона { get; set; }
        public string Электронная_почта { get; set; }
        public DateTime? Дата_регистрации { get; set; }
        public string Пароль { get; set; }

    }
}
