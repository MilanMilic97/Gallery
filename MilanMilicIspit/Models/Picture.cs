using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MilanMilicIspit.Models
{
    public class Picture
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Name { get; set; }
        [Required]
        [StringLength(70)]
        public string Author { get; set; }
        [Required]
        [Range(1520,2020)]
        public int Year { get; set; }
        [Required]
        [Range(100,50000)]
        public double Price { get; set; }
        public virtual Gallery Galery { get; set; }
        public virtual int GaleryId { get; set; }
    }
}