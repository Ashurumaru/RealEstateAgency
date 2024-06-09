using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Model
{
    internal class PhotoRealEstateModel
    {
        public int Фото_ID { get; set; }
        public int? Недвижимость_ID { get; set; }
        public byte[] Фото { get; set; }
    }
}
