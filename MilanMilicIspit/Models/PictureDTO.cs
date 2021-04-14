using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MilanMilicIspit.Models
{
    public class PictureDTO
    {
        public int Id { get; set; }
     
        public string Name { get; set; }
  
        public string Author { get; set; }

        public int Year { get; set; }

        public double Price { get; set; }

        public string GalleryName { get; set; }
    }
}