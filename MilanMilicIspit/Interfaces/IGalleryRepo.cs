using MilanMilicIspit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilanMilicIspit.Interfaces
{
    public interface IGalleryRepo
    {
        IQueryable<Gallery> GetAll();
        Gallery GetById(int id);
        void Add(Gallery gallery);
        void Update(Gallery gallery);
        void Delete(Gallery gallery);

        IQueryable<Gallery> GetAllSortedByYear();
        IQueryable<NumbersDTO> GetGalleriesWithNumberOfPictures();


    }
}
