using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MilanMilicIspit.Models
{
    public class Gallery
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public String Name { get; set; }
        [Range(1800,2021)]
        public int YearOfEstablishment { get; set; }
        public virtual IQueryable<Picture> Pictures { get; set; }
    }
}