using MilanMilicIspit.Interfaces;
using MilanMilicIspit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MilanMilicIspit.Repository
{
    public class PictureRepo : IPictureRepo, IDisposable
    {
        ApplicationDbContext db = new ApplicationDbContext();



        public IQueryable<Picture> GetAll()
        {
            return db.Pictures.OrderBy(x => x.Name);
        }

        public Picture GetById(int id)
        {
            return db.Pictures.Find(id);
        }

        public void Add(Picture picture)
        {
            db.Pictures.Add(picture);
            db.SaveChanges();
        }

        public void Update(Picture picture)
        {
            db.Entry(picture).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Picture picture)
        {
            db.Pictures.Remove(picture);
            db.SaveChanges();
        }

        public IQueryable<Picture> SearchByYear(int year)
        {
            return db.Pictures.Where(x => x.Year > year).OrderBy(x => x.Year);
        }

        public IQueryable<Picture> SearchByPrice(int min, int max)
        {
            return db.Pictures.Where(x => x.Price > min && x.Price < max).OrderByDescending(x => x.Price);
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