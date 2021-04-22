using AutoMapper;
using AutoMapper.QueryableExtensions;
using MilanMilicIspit.Interfaces;
using MilanMilicIspit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MilanMilicIspit.Controllers
{
    public class GalleriesController : ApiController
    {
        IGalleryRepo _repository { get; set; }

        public GalleriesController(IGalleryRepo repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IEnumerable<GalleryDTO> Get()
        {
            return _repository.GetAll().ProjectTo<GalleryDTO>();
        }
        //[Authorize]
        [ResponseType(typeof(GalleryDTO))]
        public IHttpActionResult GetById(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            Gallery v1 = _repository.GetById(id);
            if (v1 == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<GalleryDTO>(v1));
        }
      //  [Authorize]
        [Route("api/tradition")]
        [HttpGet]
        public IEnumerable<GalleryDTO> GetAllSortedByYear()
        {
            return _repository.GetAllSortedByYear().ProjectTo<GalleryDTO>();
        }
      //  [Authorize]
        [Route("api/number")]
        [HttpGet]
        public IEnumerable<NumbersDTO> GetGalleriesWithNumberOfPictures()
        {
            return _repository.GetGalleriesWithNumberOfPictures();
        }
    }
}
