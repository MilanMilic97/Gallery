using MilanMilicIspit.Interfaces;
using MilanMilicIspit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MilanMilicIspit.Repository
{
    public class GalleryRepo : IGalleryRepo, IDisposable
    {
        ApplicationDbContext db = new ApplicationDbContext();

        

        public IQueryable<Gallery> GetAll()
        {
            return db.Galeries.OrderBy(x => x.Name);
        }

        public Gallery GetById(int id)
        {
            return db.Galeries.Find(id);
        }

        public void Add(Gallery gallery)
        {
            throw new NotImplementedException();
        }

        public void Update(Gallery gallery)
        {
            throw new NotImplementedException();
        }

        public void Delete(Gallery gallery)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Gallery> GetAllSortedByYear()
        {
            return db.Galeries.OrderBy(x => x.YearOfEstablishment);
        }

        public IQueryable<NumbersDTO> GetGalleriesWithNumberOfPictures()
        {

            return db.Pictures.GroupBy(x => x.Galery, (gallery, id) => new NumbersDTO() { GalleryName = gallery.Name, NumberOfPictures = id.Count()});
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}