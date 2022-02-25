using Microsoft.EntityFrameworkCore;
using My_books.Data;
using My_books.Data.Model;
using My_books.Data.Services;
using My_books.Data.ViewModels;
using My_books.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace my_books_test
{
    public class PublisherServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbTest")
            .Options;
        AppDbContext context;
        PublisherService publisherService;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();
            publisherService = new PublisherService(context);
        }

        //testing GetAllPublishers method

        [Test, Order(1)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            var result = publisherService.GetAllPublishers("", "", null);
        

            Assert.That(result.Count, Is.EqualTo(5));
            //Assert.AreEqual(result.Count, 3);
        }
        
        [Test, Order(2)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearchString_WithPageNumber_Test()
        {
            var result = publisherService.GetAllPublishers("", "", 2);
        

            Assert.That(result.Count, Is.EqualTo(2));   
        }

        [Test, Order(3)]
        public void GetAllPublishers_WithNoSortBy_WithSearchString_WithNoPageNumber_Test()
        {
            var result = publisherService.GetAllPublishers("", "3", null);


            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault().Id, Is.EqualTo(3));

        }
        
        [Test, Order(4)]
        public void GetAllPublishers_WithSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            var result = publisherService.GetAllPublishers("name_desc", "", null);


            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result.FirstOrDefault().Id, Is.EqualTo(7));

        }

        //testing GetPublisherById method
        [Test, Order(5)]
        public void GetPublisherById_WithResponse_Test()
        {
            var result = publisherService.GetPublisherById(2);

            Assert.That( result.Id, Is.EqualTo(2));
            Assert.That( result.Name, Is.EqualTo("Publisher 2"));
        }

        [Test, Order(6)]
        public void GetPublisherById_WithoutResponse_Test()
        {
            var result = publisherService.GetPublisherById(99);

            Assert.That(result, Is.EqualTo(null));
        }

        //testing AddPublisher method
        [Test, Order(7)]
        public void AddPublisher_WithException_Test()
        {
            var newPublisher = new PublisherVM()
            {
                Name = "123 with Exception"
            };

            Assert.That(() => publisherService.AddPublisher(newPublisher),
                Throws.Exception.TypeOf<PublisherNameException>().With.Message.EqualTo("Name starts with number"));
        }

        [Test, Order(8)]
        public void AddPublisher_WithoutException_Test()
        {
            var newPublisher = new PublisherVM()
            {
                Name = "Without Exception"
            };

            var result = publisherService.AddPublisher(newPublisher);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Does.StartWith("Without"));
            Assert.That(result.Id, Is.Not.Null);
        }

        //testing GetPublisherData method
        [Test, Order(9)]
        public void GetPublisherData_Test()
        {
            var result = publisherService.GetPublisherData(1);

            Assert.That(result.Name, Is.EqualTo("Publisher 1"));
            Assert.That(result.BookAuthors, Is.Not.Empty);
            Assert.That(result.BookAuthors.Count, Is.GreaterThan(0));

            var firstBookName = result.BookAuthors.OrderBy(n => n.BookName).FirstOrDefault().BookName;
            Assert.That(firstBookName, Is.EqualTo("Book 1 Title"));
        }

        [Test, Order(10)]
        public void DeletePublisherById_WithResponse_Test()
        {
            publisherService.DeletePublisherById(1);
            var result = publisherService.GetPublisherById(1);
            
            Assert.That(result, Is.Null );
        }

        [Test, Order(11)]
        public void DeletePublisherById_WthoutResponse_Test()
        {

            Assert.That(() => publisherService.DeletePublisherById(99),
                Throws.Exception);
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

            var authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                }
            };
            context.Authors.AddRange(authors);


            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Book 1 Title",
                    Description = "Book 1 Description",
                    isRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                },
                new Book()
                {
                    Id = 2,
                    Title = "Book 2 Title",
                    Description = "Book 2 Description",
                    isRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                }
            };
            context.Books.AddRange(books);

            var books_authors = new List<Book_Author>()
            {
                new Book_Author()
                {
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
                },
                new Book_Author()
                {
                    Id = 2,
                    BookId = 1,
                    AuthorId = 2
                },
                new Book_Author()
                {
                    Id = 3,
                    BookId = 2,
                    AuthorId = 2
                },
            };
            context.Book_Authors.AddRange(books_authors);


            context.SaveChanges();
        }


    }
}