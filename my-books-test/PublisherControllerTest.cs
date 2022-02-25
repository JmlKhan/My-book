using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using My_books.Controllers;
using My_books.Data;
using My_books.Data.Model;
using My_books.Data.Services;
using My_books.Data.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_books_test
{
    public class PublisherControllerTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbControllerTest")
            .Options;
        AppDbContext context;
        PublisherService publisherService;
        PublishersController publisherController;
        
        [OneTimeSetUp]
        public void SetUp()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            publisherService = new PublisherService(context);
            publisherController = new PublishersController(publisherService, new NullLogger<PublishersController>());

            SeedDatabase();
        }

        [Test,Order(1)]
        public void HTTPGET_GetAllPublishers_WithSortBySearchPageNumber_ReturnOk_Test()
        {
            IActionResult actionResult = publisherController.GetAllPublishers("name_desc", "Publisher", 1);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as List<Publisher>;

            Assert.That(actionResultData.First().Name, Is.EqualTo("Publisher 7"));
            Assert.That(actionResultData.First().Id, Is.EqualTo(7));
            Assert.That(actionResultData.Count, Is.EqualTo(5));
        }

        [Test, Order(2)]
        public void HTTPGET_GetPublisherById_ReturnsOk_Test()
        {
            var actionResult = publisherController.GetPublisherById(1);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
        }

        [Test, Order(3)]
        public void HTTPGET_GetPublisherById_ReturndNotFound_Test()
        {
            var actionResult = publisherController.GetPublisherById(99);

            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());
        }

        [Test, Order(4)]
        public void HTTPPOST_AddPublisher_ReturnsCreated_Test()
        {
            var newPublisherVM = new PublisherVM()
            {
                Name = "New Publisher"
            };

            IActionResult actionResult = publisherController.AddPublisher(newPublisherVM);

            Assert.That(actionResult, Is.TypeOf<CreatedResult>());
        }
        
        [Test, Order(5)]
        public void HTTPPOST_AddPublisher_ReturnsBadRequest_Test()
        {
            var newPublisherVM = new PublisherVM()
            {
                Name = "123 Publisher"
            };

            IActionResult actionResult = publisherController.AddPublisher(newPublisherVM);

            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test, Order(6)]
        public void HTTPDELETE_DeletePublisherById_ReturnsNoContent_Test()
        {
            var actionResult = publisherController.DeletePublisherById(1);

            Assert.That(actionResult, Is.TypeOf<NoContentResult>());
        }
        
        [Test, Order(7)]
        public void HTTPDELETE_DeletePublisherById_ReturnsBadRequest_Test()
        {
            var actionResult = publisherController.DeletePublisherById(99);

            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                    new Publisher() {
                        Id = 1,
                        Name = "Publisher 1"
                    },
                    new Publisher() {
                        Id = 2,
                        Name = "Publisher 2"
                    },
                    new Publisher() {
                        Id = 3,
                        Name = "Publisher 3"
                    },
                    new Publisher() {
                        Id = 4,
                        Name = "Publisher 4"
                    },
                    new Publisher() {
                        Id = 5,
                        Name = "Publisher 5"
                    },
                    new Publisher() {
                        Id = 6,
                        Name = "Publisher 6"
                    },
                    new Publisher() {
                        Id = 7,
                        Name = "Publisher 7"
                    },
            };

            context.Publishers.AddRange(publishers);
            context.SaveChanges();
        }
    }
}
