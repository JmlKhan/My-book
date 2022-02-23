using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using My_books.Controllers;
using My_books.Data;
using My_books.Data.Model;
using My_books.Data.Services;
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

        [Test]
        public void HTTPGET_GetAllPublishers_WithSortBySearchPageNumber_ReturnOk_Test()
        {
            IActionResult actionResult = publisherController.GetAllPublishers("name_desc", "Publisher", 1);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResultData = (actionResult as OkObjectResult).Value as List<Publisher>;

            Assert.That(actionResultData.First().Name, Is.EqualTo("Publisher 7"));
            Assert.That(actionResultData.First().Id, Is.EqualTo(7));
            Assert.That(actionResultData.Count, Is.EqualTo(5));
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
