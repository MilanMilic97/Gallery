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
    public class PicturesController : ApiController
    {
        IPictureRepo _repository { get; set; }

        public PicturesController(IPictureRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<PictureDTO> Get()
        {
            return _repository.GetAll().ProjectTo<PictureDTO>();
        }

       // [Authorize]
        [ResponseType(typeof(PictureDTO))]
        public IHttpActionResult GetById(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            Picture v1 = _repository.GetById(id);
            if (v1 == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<PictureDTO>(v1));
        }

        [Route("api/getPicturesByGallery")]
        public IHttpActionResult GetByGallery(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
             return Ok(_repository.GetPicturesByGalleryId(id).ProjectTo<PictureDTO>());       
        }

        // [Authorize]
        // [Route("api/slike")]      
        public IEnumerable<PictureDTO> GetSearchByYear(int year)
        {
            return _repository.SearchByYear(year).ProjectTo<PictureDTO>();
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(picture);
            return Ok(picture);          
        }

      //  [Authorize]
        [ResponseType(typeof(PictureDTO))]
        [HttpPut]
        public IHttpActionResult Put(int id, Picture picture)
        {
            if (id < 0 || id != picture.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _repository.Update(picture);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(picture);
        }

      //  [Authorize]
        public IHttpActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            _repository.Delete(_repository.GetById(id));
            return Ok();
        }
        [Route("api/search")]
      //  [Authorize]   
        [HttpPost]
        public IEnumerable<PictureDTO> GetSearchByPrice(int min, int max)
        {
            return _repository.SearchByPrice(min,max).ProjectTo<PictureDTO>();
        }
    }
}
