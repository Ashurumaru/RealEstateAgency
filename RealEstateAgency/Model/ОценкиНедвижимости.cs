//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RealEstateAgency.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ОценкиНедвижимости
    {
        public int Оценка_ID { get; set; }
        public Nullable<int> Недвижимость_ID { get; set; }
        public Nullable<System.DateTime> Дата_оценки { get; set; }
        public Nullable<decimal> Оценочная_стоимость { get; set; }
        public string Комментарий { get; set; }
    
        public virtual Недвижимость Недвижимость { get; set; }
    }
}
