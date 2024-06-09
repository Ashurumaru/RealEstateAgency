using RealEstateAgency.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateAgency.Utils
{
    internal class DatabaseLogic
    {
        private readonly kursachEntities dbContext = new kursachEntities();

        private void SavePhotoToDatabase(int realEstateId, byte[] imageData)
        {
            if (realEstateId > 0 && imageData != null && imageData.Length > 0)
            {
                ФотографииНедвижимости photo = new ФотографииНедвижимости
                {
                    Недвижимость_ID = realEstateId,
                    Фото = imageData
                };
                dbContext.ФотографииНедвижимости.Add(photo);
                dbContext.SaveChanges();
            }
        }
        public List<Недвижимость> GetRealEstateList()
        {
            using (var context = new kursachEntities())
            {
                return context.Недвижимость.ToList();
            }
        }

        public List<ФотографииНедвижимости> GetPhotosForRealEstate(int realEstateId)
        {
            using (var context = new kursachEntities())
            {
                return context.ФотографииНедвижимости
                    .Where(p => p.Недвижимость_ID == realEstateId)
                    .ToList();
            }
        }
    }
}