using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MilanMilicIspit.Interfaces;
using MilanMilicIspit.Models;
using MilanMilicIspit.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace MilanMilicIspit.Tests
{
    [TestClass]
    public class UnitTest1
    {
        

        [TestMethod]
        public void GetReturnsProductWithSameId()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Picture, PictureDTO>() // automatski će mapirati Author.Name u AuthorName
                .ForMember(dest => dest.GalleryName, opt => opt.MapFrom(src => src.Galery.Name)); // ako želimo eksplicitno zadati mapiranje
            });

            // Arrange
            var mockRepository = new Mock<IPictureRepo>();
            mockRepository.Setup(x => x.GetById(42)).Returns(new Picture { Id = 42 });

            var controller = new PicturesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetById(42);
            var contentResult = actionResult as OkNegotiatedContentResult<PictureDTO>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.Id);
        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IPictureRepo>();
            var controller = new PicturesController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(10, new Picture { Id = 9, Name = "Product2" });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetReturnsMultipleObjects()
        {
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<Picture, PictureDTO>() // automatski će mapirati Author.Name u AuthorName
            //    .ForMember(dest => dest.GalleryName, opt => opt.MapFrom(src => src.Galery.Name)); // ako želimo eksplicitno zadati mapiranje
            //});
            // Arrange
            List<Picture> pictures = new List<Picture>();
            pictures.Add(new Picture { Id = 1, Name = "Picture1",Price = 200 ,Galery = new Gallery { Id = 1, Name = "blabla" } });
            pictures.Add(new Picture { Id = 2, Name = "Picture2", Price = 400, Galery = new Gallery { Id = 2, Name = "blabla" } });

            var mockRepository = new Mock<IPictureRepo>();
            mockRepository.Setup(x => x.SearchByPrice(100,500)).Returns(pictures.AsQueryable);
            var controller = new PicturesController(mockRepository.Object);

            // Act
            var result = controller.GetSearchByPrice(100,500);
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(pictures.Count, result.ToList().Count);
            Assert.AreEqual(pictures.ElementAt(0).Id, result.ElementAt(0).Id);
            Assert.AreEqual(pictures.ElementAt(1).Id, result.ElementAt(1).Id);
        }

        [TestMethod]
        public void GetReturnsMultipleObjectsSearch()
        {
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<Picture, PictureDTO>() // automatski će mapirati Author.Name u AuthorName
            //    .ForMember(dest => dest.GalleryName, opt => opt.MapFrom(src => src.Galery.Name)); // ako želimo eksplicitno zadati mapiranje
            //});
            // Arrange
            List<Picture> pictures = new List<Picture>();
            pictures.Add(new Picture { Id = 1, Name = "Picture1", Galery = new Gallery { Id = 1, Name = "blabla" } });
            pictures.Add(new Picture { Id = 2, Name = "Picture2", Galery = new Gallery { Id = 2, Name = "blabla" } });

            var mockRepository = new Mock<IPictureRepo>();
            mockRepository.Setup(x => x.GetAll()).Returns(pictures.AsQueryable);
            var controller = new PicturesController(mockRepository.Object);

            // Act
            var result = controller.Get();
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(pictures.Count, result.ToList().Count);
            Assert.AreEqual(pictures.ElementAt(0).Id, result.ElementAt(0).Id);
            Assert.AreEqual(pictures.ElementAt(1).Id, result.ElementAt(1).Id);
        }
    }
}
