using MilanMilicIspit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilanMilicIspit.Interfaces
{
    public interface IPictureRepo
    {
        IQueryable<Picture> GetAll();
        Picture GetById(int id);
        void Add(Picture picture);
        void Update(Picture picture);
        void Delete(Picture picture);

        IQueryable<Picture> SearchByYear(int year);
        IQueryable<Picture> SearchByPrice(int min, int max);


    }
}
